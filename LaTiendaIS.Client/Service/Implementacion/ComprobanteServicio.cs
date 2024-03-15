using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class ComprobanteServicio : IComprobanteServicio
    {
        private readonly HttpClient _httpClient;

        public ComprobanteServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Comprobante> ObtenerComprobante(int idComprobante)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<Comprobante>>($"api/Comprobante/{idComprobante}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<bool> AgregarComprobante(Comprobante Comprobante)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Comprobante", Comprobante);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        
    }
}
