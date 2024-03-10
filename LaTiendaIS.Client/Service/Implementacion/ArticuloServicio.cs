using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class ArticuloServicio: IArticuloServicio
    {
        private readonly HttpClient _httpClient;

        public ArticuloServicio(HttpClient httpClient)
        {
        _httpClient = httpClient;
        }

        public async Task<List<Articulo>> ListarArticulos()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<Articulo>>>("api/Articulo/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<Articulo> ObtenerArticulo(int idArticulo)
        {
            var result = await _httpClient.GetFromJsonAsync<Articulo>($"api/Articulo/{idArticulo}");

            if (result!=null)
                return result!;
            else
                throw new Exception(result.ToString());
        }
        

        public async Task<int> AgregarArticulo(Articulo Articulo)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Articulo", Articulo);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<int> ModificarArticulo(int idArticulo, Articulo Articulo)
        {
            HttpResponseMessage result;

            if (Articulo.IdCodigo == idArticulo)
            {
                result = await _httpClient.PutAsJsonAsync($"api/Articulo/{idArticulo}", Articulo);
            }
            else
            {
                result = await _httpClient.PostAsJsonAsync("api/Articulo", idArticulo);
            }

            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }
        public async Task<bool> EliminarArticulo(int idArticulo)
        {
            var result = await _httpClient.DeleteAsync($"api/Articulo/{idArticulo}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

    }
}
