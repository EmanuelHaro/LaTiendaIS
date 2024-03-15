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
    public class LineaDeVentaController : ControllerBase
    {
        private readonly ILineaDeVentaServicio _LineaDeVentaServicio;

        public LineaDeVentaController(ILineaDeVentaServicio LineaDeVentaServicio)
        {
            _LineaDeVentaServicio = LineaDeVentaServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarLineaDeVentas()
        {
            var responseApi = new ResponseAPI<List<LineaDeVenta>>();
            try
            {
                var listaLDV = await _LineaDeVentaServicio.ListarLineaDeVentas();

                if(listaLDV!=null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaLDV;
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
        [Route("{idLineaDeVenta}")]
        public async Task<ActionResult> ObtenerLineaDeVenta(int idLineaDeVenta)
        {
            var responseApi = new ResponseAPI<LineaDeVenta>();

            try
            {
                var lineaDeVenta = await _LineaDeVentaServicio.ObtenerLineaDeVenta(idLineaDeVenta);

                if (lineaDeVenta != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = lineaDeVenta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "LineaDeVenta no encontrado";
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
        [Route("Articulo/{idArticulo}")]
        public async Task<ActionResult> ObtenerLineaDeVentaPorArticulo(int idArticulo)
        {
            var responseApi = new ResponseAPI<LineaDeVenta>();

            try
            {
                var lineaDeVenta = await _LineaDeVentaServicio.ObtenerLineaDeVentaPorArticulo(idArticulo);

                if (lineaDeVenta != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = lineaDeVenta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "LineaDeVenta no encontrado";
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
        public async Task<ActionResult> AgregarLineaDeVenta(LineaDeVenta LineaDeVenta)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var ldvAgregada = await _LineaDeVentaServicio.AgregarLineaDeVenta(LineaDeVenta);


                if (ldvAgregada)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ldvAgregada;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al LineaDeVenta";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut("{IdLineaDeVenta}")]
        public async Task<ActionResult> ModificarLineaDeVenta(int IdLineaDeVenta, LineaDeVenta LineaDeVentaDTO)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var lineaDeVentaModificada = await _LineaDeVentaServicio.ModificarLineaDeVenta(IdLineaDeVenta, LineaDeVentaDTO);

                if (!lineaDeVentaModificada)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "LineaDeVenta no encontrado";
                    return NotFound(responseApi);
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = lineaDeVentaModificada;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejar la excepción específica de concurrencia aquí
                // Puedes agregar el código necesario para manejar esta situación
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Error de concurrencia al intentar modificar el LineaDeVenta. No se suministraron las claves primarias correctamente.";
                return StatusCode(500, responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(500, responseApi); // Internal Server Error
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("{IdLineaDeVenta}")]
        public async Task<IActionResult> EliminarLineaDeVenta(int IdLineaDeVenta)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var ldvEliminada = await _LineaDeVentaServicio.EliminarLineaDeVenta(IdLineaDeVenta);
                if (ldvEliminada)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ldvEliminada;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Valor = ldvEliminada;
                    responseApi.Mensaje = "LineaDeVenta no encontrado";
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
            var responseApi = new ResponseAPI<LineaDeVenta>();

            try
            {
                var lineaDeVenta = await _LineaDeVentaServicio.ObtenerUltimaLineaDeVenta();

                if (lineaDeVenta != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = lineaDeVenta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "LineaDeVenta no encontrado";
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

