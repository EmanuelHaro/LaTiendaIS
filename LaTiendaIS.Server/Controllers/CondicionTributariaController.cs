using AutoMapper;
using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondicionTributariaController : ControllerBase
    {
        private readonly ICondicionTServicio _CondicionTributariaServicio;

        public CondicionTributariaController(ICondicionTServicio CondicionTributariaServicio)
        {
            _CondicionTributariaServicio = CondicionTributariaServicio;
        }

        [HttpGet]
        [Route("{descCondicion}")]
        public async Task<ActionResult> ObtenerCondicionTributaria(string descCondicion)
        {
            var responseApi = new ResponseAPI<CondicionTributaria>();

            try
            {
                var condTributaria = await _CondicionTributariaServicio.ObtenerCondicionTributaria(descCondicion);

                if (condTributaria != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = condTributaria;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "CondicionTributaria no encontrado";
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