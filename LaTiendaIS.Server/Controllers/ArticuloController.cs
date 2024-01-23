using AutoMapper;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public ArticuloController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarArticulos()
        {
            var responseApi = new ResponseAPI<List<ArticuloDTO>>();
            var listaArticulosDTO = new List<ArticuloDTO>();
            try
            {
                var ArticuloDb = await _dbContext.Articulo.ToListAsync();
                foreach (var Articulo in ArticuloDb)
                {
                    listaArticulosDTO.Add(_mapper.Map<ArticuloDTO>(Articulo));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaArticulosDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpGet]
        [Route("{Codigo}")]
        public async Task<ActionResult> ObtenerArticulo(int Codigo)
        {
            var responseApi = new ResponseAPI<ArticuloDTO>();
            var ArticuloDTO = new ArticuloDTO();

            try
            {
                var dbArticulo = await _dbContext.Articulo.FirstOrDefaultAsync(f => f.Codigo == Codigo);

                if (dbArticulo != null)
                {
                    ArticuloDTO = _mapper.Map<ArticuloDTO>(dbArticulo);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ArticuloDTO;
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

        [HttpPost]
        public async Task<ActionResult> AgregarArticulo(ArticuloDTO ArticuloDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbArticulo = _mapper.Map<Articulo>(ArticuloDTO);

                _dbContext.Articulo.Add(dbArticulo);
                await _dbContext.SaveChangesAsync();

                if (dbArticulo.Codigo != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbArticulo.Codigo;
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

        [HttpPut("{legajo}")]
        public async Task<ActionResult> ModificarArticulo(int Codigo, ArticuloDTO ArticuloDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbArticulo = await _dbContext.Articulo.Where(c => c.Codigo == Codigo).FirstOrDefaultAsync();

                if (dbArticulo == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Articulo no encontrado";
                    return NotFound(responseApi);
                }

                // Update properties of dbArticulo with values from ArticuloDTO
                _mapper.Map(ArticuloDTO, dbArticulo);


                _dbContext.Entry(dbArticulo).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbArticulo.Codigo;
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
        [Route("{Codigo}")]
        public async Task<IActionResult> EliminarArticulo(int Codigo)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbArticulo = await _dbContext.Articulo.FirstOrDefaultAsync(f => f.Codigo == Codigo);
                if (dbArticulo != null)
                {
                    _dbContext.Articulo.Remove(dbArticulo);
                    await _dbContext.SaveChangesAsync();

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

