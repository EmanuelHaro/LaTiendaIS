using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace LaTiendaIS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioExternoController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private ISExternoServicio _servicioExterno;

        public ServicioExternoController(ISExternoServicio servicioExterno)
        {
            _servicioExterno = servicioExterno;
        }

        [HttpPost("Token")]
        public async Task<IActionResult> ObtenerToken()
        {
            var responseApi = new ResponseAPI<string>();

            try
            {
                var response = await _servicioExterno.ObtenerToken();
                if(response!=null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = response;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Valor = response;
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error: {ex.Message}";
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Confirmar/{token}")]
        public async Task<IActionResult> ConfirmarPago(string token)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var response = await _servicioExterno.ConfirmarPago(token);
                if (response)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = response;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Valor = response;
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error: {ex.Message}";
            }

            return Ok(responseApi);
        }
    }
}

