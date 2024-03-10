using AutoMapper;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;

using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace LaTiendaIS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioExternoController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ServicioExternoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("Token")]
        public async Task<IActionResult> ObtenerToken()
        {
            var responseApi = new ResponseAPI<string>();

            try
            {
                var requests = new TokenRequest
                {
                    card_number = "4507990000004905",
                    card_expiration_month = "08",
                    card_expiration_year = "24",
                    security_code = "123",
                    card_holder_name = "John Doe",
                    card_holder_identification = new CardHolderIdentification
                    {
                        type = "dni",
                        number = "25123456"
                    }
                };

                var jsonRequest = JsonSerializer.Serialize(requests);
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://developers.decidir.com/api/v2/tokens");
                httpRequest.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                httpRequest.Headers.Add("apikey", "b192e4cb99564b84bf5db5550112adea");

                var response = await _httpClient.SendAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(jsonResponse);

                    responseApi.Valor = tokenResponse.id;
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = $"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error: {ex.Message}";
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Confirmar/{token}")]
        public async Task<IActionResult> ConfirmarPago(string token)
        {
            var responseApi = new ResponseAPI<bool>();

            var apiKey = "566f2c897b5e4bfaa0ec2452f5d67f13"; // Clave API proporcionada por el proveedor

            // Construir el cuerpo de la solicitud en formato JSON
            var requestBody = JsonSerializer.Serialize(new PaymentRequest
            {
                site_transaction_id = Guid.NewGuid().ToString(), // Genera un ID de transacción único
                payment_method_id = 1,
                token = token,
                bin = "450799",
                amount = 1000,
                currency = "ARS",
                installments = 1,
                description = "",
                payment_type = "single",
                establishment_name = "single",
                sub_payments = new List<SubPayment> // Usando una lista en lugar de un solo objeto SubPayment
                {
                    new SubPayment
                    {
                        site_id = "",
                        amount = 1000,
                        installments = null
                    }
                }
            });

            // Configurar la solicitud HTTP
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://developers.decidir.com/api/v2/payments?");
            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Agregar la clave API al encabezado
            httpRequest.Headers.Add("apikey", apiKey);

            // Enviar la solicitud HTTP
            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                // Manejar la respuesta del pago
                responseApi.EsCorrecto = true;
            }
            else
            {
                // Manejar el error de confirmación de pago
                responseApi.EsCorrecto = false;
            }

            return Ok(responseApi);
        }
    }
}

