using Azure;
using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Implementacion
{
    public class SExternoServicio : ISExternoServicio
    { 

        private readonly HttpClient _httpClient;
        public SExternoServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ObtenerToken()
        {
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

                    return tokenResponse.id;
                }
                else
                {
                    throw new TaskCanceledException($"Error en la solicitud {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> ConfirmarPago(string token)
        {
            try
            {
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
                    return true;
                }
                else
                {
                    // Manejar el error de confirmación de pago
                    return false;
                }
            }
            catch(Exception ex) 
            {
                throw;
            }
        }

    }
}
