using LaTiendaIS.Shared;
using System.Net.Http;
using LaTiendaIS.Client.Service.Contrato;
using System.Net.Http.Json;
using Azure;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class StockServicio: IStockServicio
    {
        private readonly HttpClient _httpClient;

        public StockServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> AgregarCantidadStock(int codigoTienda, string talle, string color, Stock stock)
        {
            HttpResponseMessage result;

            result = await _httpClient.PutAsJsonAsync($"api/Stock/Agregar/{codigoTienda}/{talle}/{color}", stock);

            if (result != null)
            {
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
            else
            {
                throw new Exception(result.StatusCode.ToString());
            }
        }

        public async Task<int> ModificarCantidadStock(int codigoTienda, string talle, string color, Stock stock)
        {
            HttpResponseMessage result;

            result = await _httpClient.PutAsJsonAsync($"api/Stock/Modificar/{codigoTienda}/{talle}/{color}", stock);

            if(result!=null)
            {
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
            else
            {
                throw new Exception(result.StatusCode.ToString());
            }
        }
            

        public async Task<int> ObtenerCantidad(int codigoTienda, string talle, string color)
        {

                var result = await _httpClient.GetFromJsonAsync<ResponseAPI<int>>($"api/Stock/{codigoTienda}/{talle}/{color}");

                if (result!.EsCorrecto)
                    return result.Valor!;
                else
                    throw new Exception(result.Mensaje);
            }

        public async Task<List<Stock>> ObtenerStock(int idArticulo)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<Stock>>>($"api/Stock/{idArticulo}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}
