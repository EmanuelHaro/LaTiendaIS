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
            var responseApi = new ResponseAPI<List<Articulo>>();
            var listaArticulosDTO = new List<Articulo>();
            try
            {
                var ArticuloDb = await _dbContext.Articulo.ToListAsync();
                foreach (var Articulo in ArticuloDb)
                {
                    Articulo.Marca = _dbContext.Marca.Find(Articulo.IdMarca);
                    Articulo.Categoria = _dbContext.Categoria.Find(Articulo.IdCategoria);
                    listaArticulosDTO.Add(_mapper.Map<Articulo>(Articulo));
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
        [Route("{IdCodigo}")]
        public async Task<ActionResult> ObtenerArticulo(int IdCodigo) //antes pasaba talle y color como parametro
        {
            var responseApi = new ResponseAPI<Articulo>();
            var ArticuloDTO = new Articulo();

            try
            {
                var dbArticulo = await _dbContext.Articulo.FirstOrDefaultAsync(f => f.CodigoTienda == IdCodigo);
                

                if (dbArticulo != null)
                {
                    
                    dbArticulo.Marca = _dbContext.Marca.Find(dbArticulo.IdMarca);
                    dbArticulo.Categoria = _dbContext.Categoria.Find(dbArticulo.IdCategoria);

                    ArticuloDTO = _mapper.Map<Articulo>(dbArticulo);

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

        [HttpGet]
        [Route("Stock/{IdCodigo}")]
        public async Task<ActionResult> ObtenerListaDeTalleYColorDelStock(int IdCodigo)
        {
            var responseApi = new ResponseAPI<List<Stock>>();
            var ArticuloDTO = new Articulo();
            var Stock = new List<Stock>();

            try
            {
                var dbStock = await _dbContext.Stock.Where(a=>a.IdArticulo == IdCodigo).ToListAsync();
               

                if (dbStock != null)
                {
                    foreach(var stockItem in dbStock)
                    {
                        stockItem.Talle = _dbContext.Talle.Find(stockItem.IdTalle);
                        stockItem.Color = _dbContext.ColorArticulo.Find(stockItem.IdColor);
                    }
                    Stock = _mapper.Map<List<Stock>>(dbStock);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Stock;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Stock no coincide";
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
        public async Task<ActionResult> AgregarArticulo(Articulo ArticuloDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbArticulo = _mapper.Map<ArticuloDTO>(ArticuloDTO);
                

                _dbContext.Articulo.Add(dbArticulo);
                await _dbContext.SaveChangesAsync();

                if (dbArticulo.IdCodigo != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbArticulo.IdCodigo;
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
        public async Task<ActionResult> ModificarArticulo(int IdCodigo, Articulo ArticuloDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbArticulo = await _dbContext.Articulo.Where(c => c.CodigoTienda == IdCodigo).FirstOrDefaultAsync();

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
                responseApi.Valor = dbArticulo.IdCodigo;
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
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbArticulo = await _dbContext.Articulo.FirstOrDefaultAsync(f => f.IdCodigo == IdCodigo);
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

