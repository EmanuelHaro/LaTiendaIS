using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class PagoEfectivoServicio: IPagoEfectivoServicio
    {
        private readonly HttpClient _httpClient;

        public PagoEfectivoServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AgregarPago(PagoEfectivo Pago)
        {
            var result = await _httpClient.PostAsJsonAsync("api/PagoConEfectivo", Pago);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<Pago> ObtenerPago(int idPago)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<Pago>>($"api/PagoConEfectivo/{idPago}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}
