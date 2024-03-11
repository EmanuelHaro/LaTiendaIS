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

        //[HttpGet]
        //public async Task<IActionResult> ListarArticulos()
        //{
        //    var responseApi = new ResponseAPI<List<Articulo>>();
        //    var listaArticulosDTO = new List<Articulo>();
        //    try
        //    {
        //        var ArticuloDb = await _dbContext.Articulo.ToListAsync();
        //        foreach (var Articulo in ArticuloDb)
        //        {
        //            Articulo.Marca = _dbContext.Marca.Find(Articulo.IdMarca);
        //            Articulo.Categoria = _dbContext.Categoria.Find(Articulo.IdCategoria);
        //            listaArticulosDTO.Add(_mapper.Map<Articulo>(Articulo));
        //        }

        //        responseApi.EsCorrecto = true;
        //        responseApi.Valor = listaArticulosDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }

        //    return Ok(responseApi);
        //}

        //[HttpPost]
        //public async Task<ActionResult> AgregarArticulo(Articulo ArticuloDTO)
        //{
        //    var responseApi = new ResponseAPI<int>();
        //    try
        //    {
        //        var dbArticulo = _mapper.Map<ArticuloDTO>(ArticuloDTO);
                

        //        _dbContext.Articulo.Add(dbArticulo);
        //        await _dbContext.SaveChangesAsync();

        //        if (dbArticulo.IdCodigo != 0)
        //        {
        //            responseApi.EsCorrecto = true;
        //            responseApi.Valor = dbArticulo.IdCodigo;
        //        }
        //        else
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "No se pudo guardar al Articulo";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }

        //    return Ok(responseApi);
        //}

        //[HttpPut("{IdCodigo}")]
        //public async Task<ActionResult> ModificarArticulo(int IdCodigo, Articulo ArticuloDTO)
        //{
        //    var responseApi = new ResponseAPI<int>();

        //    try
        //    {
        //        var dbArticulo = await _dbContext.Articulo.Where(c => c.CodigoTienda == IdCodigo).FirstOrDefaultAsync();

        //        if (dbArticulo == null)
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "Articulo no encontrado";
        //            return NotFound(responseApi);
        //        }

        //        // Update properties of dbArticulo with values from ArticuloDTO
        //        _mapper.Map(ArticuloDTO, dbArticulo);


        //        _dbContext.Entry(dbArticulo).State = EntityState.Modified;
        //        await _dbContext.SaveChangesAsync();

        //        responseApi.EsCorrecto = true;
        //        responseApi.Valor = dbArticulo.IdCodigo;
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        // Manejar la excepción específica de concurrencia aquí
        //        // Puedes agregar el código necesario para manejar esta situación
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = "Error de concurrencia al intentar modificar el Articulo. No se suministraron las claves primarias correctamente.";
        //        return StatusCode(500, responseApi);
        //    }
        //    catch (Exception ex)
        //    {
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //        return StatusCode(500, responseApi); // Internal Server Error
        //    }

        //    return Ok(responseApi);
        //}

        //[HttpDelete]
        //[Route("{IdCodigo}")]
        //public async Task<IActionResult> EliminarArticulo(int IdCodigo)
        //{
        //    var responseApi = new ResponseAPI<int>();

        //    try
        //    {
        //        var dbArticulo = await _dbContext.Articulo.FirstOrDefaultAsync(f => f.IdCodigo == IdCodigo);
        //        if (dbArticulo != null)
        //        {
        //            _dbContext.Articulo.Remove(dbArticulo);
        //            await _dbContext.SaveChangesAsync();

        //            responseApi.EsCorrecto = true;
        //        }
        //        else
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "Articulo no encontrado";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }

        //    return Ok(responseApi);
        //}

    }
}

