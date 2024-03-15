using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class PagoServicio: IPagoServicio
    {
        private readonly HttpClient _httpClient;

        public PagoServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Pago> ObtenerConVentaPago(int idVenta)
        {

            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<Pago>>($"api/Pago/{idVenta}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
            
        }
    }
}
