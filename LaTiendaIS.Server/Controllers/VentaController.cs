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
    public class VentaController : ControllerBase
    {
        private readonly IVentaServicio _VentaServicio;

        public VentaController(IVentaServicio VentaServicio)
        {
            _VentaServicio = VentaServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarVentas()
        {
            var responseApi = new ResponseAPI<List<Venta>>();
            
            try
            {
                var lista = await _VentaServicio.ListarVentas();

                if(lista!=null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = lista;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Valor = null;
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
        [Route("{idVenta}")]
        public async Task<ActionResult> ObtenerVenta(int idVenta)
        {
            var responseApi = new ResponseAPI<Venta>();

            try
            {
                var venta = await _VentaServicio.ObtenerVenta(idVenta);

                if (venta != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = venta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrada";
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
        [Route("Ultima")]
        public async Task<ActionResult> ObtenerUltimaVenta()
        {
            var responseApi = new ResponseAPI<Venta>();

            try
            {
                var ultimaVenta = await _VentaServicio.ObtenerUltimaVenta();

                if (ultimaVenta != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ultimaVenta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Ultima Venta no encontrada";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        public async Task<ActionResult> AgregarVenta(Venta Venta)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                
                var ventaAgregada = await _VentaServicio.AgregarVenta(Venta);

                if (ventaAgregada)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ventaAgregada;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al Venta";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut("{IdVenta}")]
        public async Task<ActionResult> ModificarVenta(int IdVenta, Venta venta)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var ventaModificada = await _VentaServicio.ModificarVenta(IdVenta, venta);
   
                if (!ventaModificada)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrado";
                    return NotFound(responseApi);
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = ventaModificada;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Error de concurrencia al intentar modificar el Venta. No se suministraron las claves primarias correctamente.";
                return StatusCode(500, responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(500, responseApi); 
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("{IdVenta}")]
        public async Task<IActionResult> EliminarVenta(int IdVenta)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var ventaELiminada = await _VentaServicio.EliminarVenta(IdVenta);

                if (ventaELiminada)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ventaELiminada;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

    }
}

