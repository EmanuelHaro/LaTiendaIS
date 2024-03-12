using AutoMapper;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaTiendaIS.ServiciosAPI.Contrato;

namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloServicio _articuloServicio;
        
        public ArticuloController(IArticuloServicio articuloServicio)
        {
            _articuloServicio = articuloServicio;
        }

        [HttpGet]
        [Route("{IdCodigo}")]
        public async Task<ActionResult> ObtenerArticulo(int IdCodigo) 
        {
            var responseApi = new ResponseAPI<Articulo>();

            try
            {
                var dbArticulo = await _articuloServicio.ObtenerArticulo(IdCodigo);             

                if (dbArticulo != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbArticulo;
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

        [HttpGet]
        public async Task<IActionResult> ListarArticulos()
        {
            var responseApi = new ResponseAPI<List<Articulo>>();
            try
            {
                var lista = await _articuloServicio.ListarArticulos();
                
                responseApi.EsCorrecto = true;
                responseApi.Valor = lista;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        public async Task<ActionResult> AgregarArticulo(Articulo Articulo)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var articuloCreado = await _articuloServicio.AgregarArticulo(Articulo);

                if (articuloCreado)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = articuloCreado;
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

        [HttpPut("{IdCodigo}")]
        public async Task<ActionResult> ModificarArticulo(int IdCodigo, Articulo Articulo)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var articuloModificado = await _articuloServicio.ModificarArticulo(IdCodigo, Articulo);
                if (articuloModificado)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = articuloModificado;
                }
                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejar la excepción específica de concurrencia aquí
                // Puedes agregar el código necesario para manejar esta situación
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Error de concurrencia al intentar modificar el Articulo. No se suministraron las claves primarias correctamente.";
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
        [Route("{IdCodigo}")]
        public async Task<IActionResult> EliminarArticulo(int IdCodigo)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var articuloEliminado = await _articuloServicio.EliminarArticulo(IdCodigo);
                if (articuloEliminado)
                {
                    responseApi.Valor = articuloEliminado;
                    responseApi.EsCorrecto = true;
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

            return Ok(responseApi);
        }

    }
}

