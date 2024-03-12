using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class LineaDeVentaServicio : ILineaDeVentaServicio
    {
        private readonly HttpClient _httpClient;

        public LineaDeVentaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LineaDeVenta>> ListarLineaDeVentas()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<LineaDeVenta>>>("api/LineaDeVenta/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<LineaDeVenta> ObtenerLineaDeVenta(int idLineaDeVenta)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<LineaDeVenta>>($"api/LineaDeVenta/{idLineaDeVenta}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<LineaDeVenta> ObtenerUltimaLineaDeVenta()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<LineaDeVenta>>("api/LineaDeVenta/Ultima");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<bool> AgregarLineaDeVenta(LineaDeVenta LineaDeVenta)
        {
            var result = await _httpClient.PostAsJsonAsync("api/LineaDeVenta", LineaDeVenta);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<bool> ModificarLineaDeVenta(int idLineaDeVenta, LineaDeVenta LineaDeVenta)
        {
            HttpResponseMessage result;

            if (LineaDeVenta.IdLineaDeVenta == idLineaDeVenta)
            {
                // Si el cliente tiene un Id, se trata de una modificación
                result = await _httpClient.PutAsJsonAsync($"api/Cliente/{idLineaDeVenta}", LineaDeVenta);
            }
            else
            {
                // Si el cliente no tiene un Id válido, se trata de una adición
                result = await _httpClient.PostAsJsonAsync("api/Cliente", idLineaDeVenta);
            }

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
        public async Task<bool> EliminarLineaDeVenta(int idLineaDeVenta)
        {
            var result = await _httpClient.DeleteAsync($"api/LineaDeVenta/{idLineaDeVenta}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

    }
}
