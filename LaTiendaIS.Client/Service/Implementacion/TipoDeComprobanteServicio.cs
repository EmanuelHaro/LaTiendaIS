using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class TipoDeComprobanteServicio: ITipoDeComprobante
    {
        private readonly HttpClient _httpClient;

        public TipoDeComprobanteServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TipoDeComprobante> ObtenerComprobanteConCondicionTributaria(string descCondicion)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<TipoDeComprobante>>($"api/TipoDeComprobante/Factura/{descCondicion}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

    }
}
