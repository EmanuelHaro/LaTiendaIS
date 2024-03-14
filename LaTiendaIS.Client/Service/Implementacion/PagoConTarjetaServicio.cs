using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class PagoConTarjetaServicio : IPagoConTarjetaServicio
    {
        private readonly HttpClient _httpClient;

        public PagoConTarjetaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AgregarPago(PagoConTarjeta Pago)
        {
            var result = await _httpClient.PostAsJsonAsync("api/PagoConTarjeta", Pago);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<Pago> ObtenerPago(int idPago)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<Pago>>($"api/Pago/{idPago}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}
