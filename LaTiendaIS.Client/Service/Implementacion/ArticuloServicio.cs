﻿using LaTiendaIS.Client.Service.Contrato;
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

        public async Task<List<ArticuloDTO>> ListarArticulos()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<ArticuloDTO>>>("api/Articulo/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<ArticuloDTO> ObtenerArticulo(int idArticulo)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<ArticuloDTO>>($"api/Articulo/{idArticulo}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<int> AgregarArticulo(ArticuloDTO Articulo)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Articulo", Articulo);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<int> ModificarArticulo(int idArticulo, ArticuloDTO Articulo)
        {
            HttpResponseMessage result;

            if (Articulo.IdCodigo == idArticulo)
            {
                // Si el cliente tiene un Id, se trata de una modificación
                result = await _httpClient.PutAsJsonAsync($"api/Cliente/{idArticulo}", Articulo);
            }
            else
            {
                // Si el cliente no tiene un Id válido, se trata de una adición
                result = await _httpClient.PostAsJsonAsync("api/Cliente", idArticulo);
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