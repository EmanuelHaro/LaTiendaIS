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
    public class SucursalController : ControllerBase
    {
        private readonly ISucursalServicio _SucursalServicio;

        public SucursalController(ISucursalServicio SucursalServicio)
        {
            _SucursalServicio = SucursalServicio;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarSucursal(Sucursal Sucursal)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var SucursalAgregada = await _SucursalServicio.AgregarSucursal(Sucursal);

                if (SucursalAgregada)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = SucursalAgregada;
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
        [Route("{descSucursal}")]
        public async Task<ActionResult> ObtenerSucursal(string descSucursal) //antes pasaba talle y color como parametro
        {
            var responseApi = new ResponseAPI<Sucursal>();

            try
            {
                var Sucursal = await _SucursalServicio.ObtenerSucursal(descSucursal);

                if (Sucursal != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Sucursal;
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
