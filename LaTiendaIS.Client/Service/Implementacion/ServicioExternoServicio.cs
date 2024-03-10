using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class ServicioExternoServicio : IServicioExternoServicio
    {
        private readonly HttpClient _httpClient;

        public ServicioExternoServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ObtenerToken()
        {
            TokenRequest token = new TokenRequest();
            var response = await _httpClient.PostAsJsonAsync("api/ServicioExterno/token", token);
            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<ResponseAPI<string>>();
            if (tokenResponse!.EsCorrecto)
                return tokenResponse.Valor!;
            else
                throw new Exception(tokenResponse.Mensaje);
        }

        public async Task<bool> ConfirmarPago(string token)
        {
            var paymentRequest = new PaymentRequest();
            
            var response = await _httpClient.PostAsJsonAsync($"api/ServicioExterno/Confirmar/{token}", paymentRequest);
            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<ResponseAPI<bool>>();
            if (tokenResponse!.EsCorrecto)
                return tokenResponse.EsCorrecto!;
            else
                throw new Exception(tokenResponse.Mensaje);
        }

    }
}
