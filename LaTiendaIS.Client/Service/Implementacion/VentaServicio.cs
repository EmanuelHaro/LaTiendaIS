using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using System.Net.Http.Json;

namespace LaTiendaIS.Client.Service.Implementacion
{
    public class VentaServicio : IVentaServicio
    {
        private readonly HttpClient _httpClient;

        public VentaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VentaDTO>> ListarVentas()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<VentaDTO>>>("api/Venta/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<VentaDTO> ObtenerVenta(int idVenta)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<VentaDTO>>($"api/Venta/{idVenta}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> AgregarVenta(VentaDTO Venta)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Venta", Venta);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<int> ModificarVenta(int idVenta, VentaDTO Venta)
        {
            HttpResponseMessage result;

            if (Venta.IdVenta == idVenta)
            {
                // Si el cliente tiene un Id, se trata de una modificación
                result = await _httpClient.PutAsJsonAsync($"api/Venta/{idVenta}", Venta);
            }
            else
            {
                // Si el cliente no tiene un Id válido, se trata de una adición
                result = await _httpClient.PostAsJsonAsync("api/Venta", idVenta);
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
        public async Task<bool> EliminarVenta(int idVenta)
        {
            var result = await _httpClient.DeleteAsync($"api/Venta/{idVenta}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }




    }
}
