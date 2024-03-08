//using AutoMapper;
//using LaTiendaIS.Shared;
//using LaTiendaIS.Shared.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http;
//using System.Text.Json;
//using System.Text;

//namespace LaTiendaIS.Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ServicioExternoController : ControllerBase
//    {
//        private DBLaTiendaContext _dbContext;
//        private readonly IMapper _mapper;

//        public ServicioExternoController(DBLaTiendaContext dbContext, IMapper mapper)
//        {
//            _dbContext = dbContext;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        [Route("SolicitarToken")]
//        public async Task<string> SolicitarTokenPago()
//        {
//            // Crear la solicitud con los datos necesarios
//            var request = new TokenRequest
//            {
//                card_number = "4507990000004905",
//                card_expiration_month = "08",
//                card_expiration_year = "24",
//                security_code = "123",
//                card_holder_name = "John Doe",
//                card_holder_identification = new CardHolderIdentification
//                {
//                    type = "dni",
//                    number = "25123456"
//                }
//            };

//            // Convertir la solicitud a JSON
//            var jsonRequest = JsonSerializer.Serialize(request);

//            // Configurar la solicitud HTTP
//            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://developers.decidir.com/api/v2/tokens");
//            httpRequest.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

//            // Agregar la clave API al encabezado
//            httpRequest.Headers.Add("apikey", "b192e4cb99564b84bf5db5550112adea");

//            // Enviar la solicitud HTTP
//            var response = await _httpClient.SendAsync(httpRequest);

//            // Verificar si la solicitud fue exitosa
//            if (response.IsSuccessStatusCode)
//            {
//                // Leer y deserializar la respuesta JSON
//                var jsonResponse = await response.Content.ReadAsStringAsync();
//                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(jsonResponse);

//                // Retornar el ID del token
//                return tokenResponse.id;
//            }
//            else
//            {
//                // Manejar errores de solicitud
//                Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
//                return null;
//            }
//        }

//        [HttpGet]
//        [Route("Confirmar")]
//        private async Task<bool> ConfirmarPago(string token)
//        {
//            var apiKey = "566f2c897b5e4bfaa0ec2452f5d67f13"; // Clave API proporcionada por el proveedor

//            // string creditCardWithoutSpaces = creditCard.Replace(" ", "");
//            // string bin = creditCardWithoutSpaces.Substring(0, 6);

//            // Construir el cuerpo de la solicitud en formato JSON
//            var requestBody = JsonSerializer.Serialize(new PaymentRequest
//            {
//                site_transaction_id = Guid.NewGuid().ToString(), // Genera un ID de transacción único
//                payment_method_id = 1,
//                token = token,
//                bin = "450799",
//                amount = 1000,
//                currency = "ARS",
//                installments = 1,
//                description = "",
//                payment_type = "single",
//                establishment_name = "single",
//                sub_payments = new List<SubPayment> // Usando una lista en lugar de un solo objeto SubPayment
//                {
//                    new SubPayment
//                    {
//                        site_id = "",
//                        amount = 1000,
//                        installments = null
//                    }
//                }
//            });

//            // Configurar la solicitud HTTP
//            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://developers.decidir.com/api/v2/payments?");
//            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

//            // Agregar la clave API al encabezado
//            httpRequest.Headers.Add("apikey", apiKey);

//            // Enviar la solicitud HTTP
//            var response = await httpClient.SendAsync(httpRequest);

//            if (response.IsSuccessStatusCode)
//            {
//                // Manejar la respuesta del pago
//                return true;
//            }
//            else
//            {
//                // Manejar el error de confirmación de pago
//                return false;
//            }
//        }
//    }
//}
