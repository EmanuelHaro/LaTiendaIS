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
    public class TalleController : ControllerBase
    {
        private readonly ITalleServicio _TalleServicio;

        public TalleController(ITalleServicio TalleServicio)
        {
            _TalleServicio = TalleServicio;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarTalle(Talle Talle)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var TalleAgregada = await _TalleServicio.AgregarTalle(Talle);

                if (TalleAgregada)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TalleAgregada;
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
        [Route("{descTalle}")]
        public async Task<ActionResult> ObtenerTalle(string descTalle)
        {
            var responseApi = new ResponseAPI<Talle>();

            try
            {
                var Talle = await _TalleServicio.ObtenerTalle(descTalle);

                if (Talle != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Talle;
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
