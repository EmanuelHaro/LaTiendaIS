using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class CondicionTributariaServicio: ICondicionTServicio
    {
        private readonly HttpClient _httpClient;

        public CondicionTributariaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<CondicionTributaria> ObtenerCondicionTributaria(string descCondicion)
        {
            var result = await _httpClient.GetFromJsonAsync<CondicionTributaria>($"api/CondicionTributaria/{descCondicion}");

            if (result != null)
                return result!;
            else
                throw new Exception(result.ToString());
        }
    }
}
