using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorArticuloController : ControllerBase
    {
        private readonly IColorArticuloServicio _ColorArticuloServicio;

        public ColorArticuloController(IColorArticuloServicio ColorArticuloServicio)
        {
            _ColorArticuloServicio = ColorArticuloServicio;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarColorArticulo(ColorArticulo ColorArticulo)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var ColorAgregado = await _ColorArticuloServicio.AgregarColorArticulo(ColorArticulo);

                if (ColorAgregado)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ColorAgregado;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al Articulo";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("{descColorArticulo}")]
        public async Task<ActionResult> ObtenerColorArticulo(string descColorArticulo)
        {
            var responseApi = new ResponseAPI<ColorArticulo>();

            try
            {
                var ColorArticulo = await _ColorArticuloServicio.ObtenerColorArticulo(descColorArticulo);

                if (ColorArticulo != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ColorArticulo;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Articulo no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi.Valor);
        }
    }
}
