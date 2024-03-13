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
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaServicio _MarcaServicio;

        public MarcaController(IMarcaServicio MarcaServicio)
        {
            _MarcaServicio = MarcaServicio;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarMarca(Marca Marca)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var MarcaAgregada = await _MarcaServicio.AgregarMarca(Marca);

                if (MarcaAgregada)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = MarcaAgregada;
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
        [Route("{descMarca}")]
        public async Task<ActionResult> ObtenerMarca(string descMarca) 
        {
            var responseApi = new ResponseAPI<Marca>();

            try
            {
                var Marca = await _MarcaServicio.ObtenerMarca(descMarca);

                if (Marca != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Marca;
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
