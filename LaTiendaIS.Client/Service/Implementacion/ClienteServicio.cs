using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly HttpClient _httpClient;

        public ClienteServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Cliente> ObtenerCliente(int idCliente)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<Cliente>>($"api/Cliente/{idCliente}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarCliente(Cliente Cliente)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Cliente", Cliente);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<Cliente> ObtenerUltimaCliente()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<Cliente>>("api/Cliente/Ultima");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

    }
}
