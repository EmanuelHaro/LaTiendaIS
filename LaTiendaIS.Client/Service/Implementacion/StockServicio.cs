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

        public async Task<bool> AgregarCantidadStock(int codigoTienda, string talle, string color, Stock stock)
        {
            HttpResponseMessage result;

            result = await _httpClient.PutAsJsonAsync($"api/Stock/Agregar/{codigoTienda}/{talle}/{color}", stock);

            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

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

        public async Task<bool> ModificarCantidadStock(int codigoTienda, string talle, string color, Stock stock)
        {
            HttpResponseMessage result;

            result = await _httpClient.PutAsJsonAsync($"api/Stock/Modificar/{codigoTienda}/{talle}/{color}", stock);

            if(result!=null)
            {
                var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

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

                var result = await _httpClient.GetFromJsonAsync<int>($"api/Stock/Cantidad/{codigoTienda}/{talle}/{color}");

                if (result!=null)
                    return result!;
                else
                    throw new Exception(result.ToString());
            }

        public async Task<List<Stock>> ObtenerListaDeTalleYColorDelStock(int idArticulo)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Stock>>($"api/Stock/Articulo/{idArticulo}");

            if (result != null)
                return result!;
            else
                throw new Exception(result.ToString());
        }

        public async Task<Stock> ObtenerStockPorArticulo(int codigoTienda, string talle, string color)
        {

            var result = await _httpClient.GetFromJsonAsync<Stock>($"api/Stock/{codigoTienda}/{talle}/{color}");

            if (result != null)
                return result!;
            else
                throw new Exception(result.ToString());
        }

        public async Task<Stock> ObtenerStockPorId(int idStock)
        {
            var result = await _httpClient.GetFromJsonAsync<Stock>($"api/Stock/{idStock}");

            if (result != null)
                return result!;
            else
                throw new Exception(result.ToString());
        }
    }
}
