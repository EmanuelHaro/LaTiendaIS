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

        public async Task<List<LineaDeVentaDTO>> ListarLineaDeVentas()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<LineaDeVentaDTO>>>("api/LineaDeVenta/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<LineaDeVentaDTO> ObtenerLineaDeVenta(int idLineaDeVenta)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<LineaDeVentaDTO>>($"api/LineaDeVenta/{idLineaDeVenta}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<LineaDeVentaDTO> ObtenerUltimaLineaDeVenta()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<LineaDeVentaDTO>>("api/LineaDeVenta/Ultima");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarLineaDeVenta(LineaDeVentaDTO LineaDeVenta)
        {
            var result = await _httpClient.PostAsJsonAsync("api/LineaDeVenta", LineaDeVenta);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<int> ModificarLineaDeVenta(int idLineaDeVenta, LineaDeVentaDTO LineaDeVenta)
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
        public async Task<bool> EliminarLineaDeVenta(int idLineaDeVenta)
        {
            var result = await _httpClient.DeleteAsync($"api/LineaDeVenta/{idLineaDeVenta}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

    }
}
