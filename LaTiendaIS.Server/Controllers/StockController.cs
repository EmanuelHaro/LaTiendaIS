using AutoMapper;
using AutoMapper.Configuration.Conventions;
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
    public class StockController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public StockController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListarStock()
        {
            var responseApi = new ResponseAPI<List<Stock>>();
            var listaStock = new List<Stock>();
            try
            {
                var stockdb = await _dbContext.Stock.ToListAsync();
                foreach (var stock in stockdb)
                {
                    listaStock.Add(_mapper.Map<Stock>(stock));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaStock;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi.Valor);
        }



        [HttpGet]
        [Route("{codigoTienda}")] //localhost:5020/api/Stock/1000
        public async Task<ActionResult> ObtenerListaDeTalleYColorDelStock(int codigoTienda)
        {
            var responseApi = new ResponseAPI<List<Stock>>();
            var Stock = new List<Stock>();

            try
            {
                var dbArticulo = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();
                if (dbArticulo != null)
                {
                    var dbStock = await _dbContext.Stock.Where(a => a.IdArticulo == dbArticulo.IdCodigo && a.Cantidad > 0).ToListAsync();


                    if (dbStock != null)
                    {
                        foreach (var stockItem in dbStock)
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
                        responseApi.Mensaje = "Articulo no encontrado en Stock/Cantidad insuficiente";
                    }

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Codigo de Articulo no coincide";
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
        [Route("{codigoTienda}/{talle}/{color}")]
        public async Task<ActionResult> ObtenerCantidad(int codigoTienda,string talle, string color)
        {
            var responseApi = new ResponseAPI<int>();
            var Stock = new List<Stock>();

            try
            {
                var dbTalle = await _dbContext.Talle.Where(t=>t.DescripcionTalle == talle).FirstOrDefaultAsync();
                var dbColor = await _dbContext.ColorArticulo.Where(t => t.DescripcionColor == color).FirstOrDefaultAsync();
                var dbArticulo = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();

                if (dbTalle != null && dbColor != null)
                {
                    var dbStock = await _dbContext.Stock.Where(cant => cant.IdTalle == dbTalle.IdTalle && cant.IdColor == dbColor.IdColor && cant.IdArticulo == dbArticulo.IdCodigo).FirstOrDefaultAsync();

                    if (dbStock != null)
                    {
                        responseApi.EsCorrecto = true;
                        responseApi.Valor = dbStock.Cantidad;
                    }
                    else
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensaje = "No coinciden talle y color";
                    }
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpPut]
        [Route("Modificar/{codigoTienda}/{talle}/{color}")]
        public async Task<ActionResult> ModificarCantidad(int codigoTienda, string talle, string color, Stock stock)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbTalle = await _dbContext.Talle.Where(t => t.DescripcionTalle == talle).FirstOrDefaultAsync();
                var dbColor = await _dbContext.ColorArticulo.Where(t => t.DescripcionColor == color).FirstOrDefaultAsync();
                var dbArticulos = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();

                if (dbTalle != null && dbColor != null)
                {
                    var dbStock = await _dbContext.Stock.Where(cant => cant.IdTalle == dbTalle.IdTalle && cant.IdColor == dbColor.IdColor && cant.IdArticulo == dbArticulos.IdCodigo).FirstOrDefaultAsync();

                    if (dbStock == null)
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensaje = "Stock no encontrado";
                        return NotFound(responseApi);
                    }
                    //Cambiar propiedades de stock
                    if(dbStock.Cantidad - stock.Cantidad >= 0)
                    {
                        stock.Cantidad = dbStock.Cantidad - stock.Cantidad;
                        stock.IdSucursal = dbStock.IdSucursal;
                        stock.IdArticulo = dbStock.IdArticulo;
                        stock.IdTalle = dbStock.IdTalle;
                        stock.IdColor = dbStock.IdColor;
                        stock.IdStock = dbStock.IdStock;
                        stock.Articulo = null;
                        stock.Color = null;
                        stock.Talle = null;

                        // Update properties of dbArticulo with values from ArticuloDTO
                        _mapper.Map(stock, dbStock);


                        _dbContext.Entry(dbStock).State = EntityState.Modified;
                        await _dbContext.SaveChangesAsync();

                        responseApi.EsCorrecto = true;
                        responseApi.Valor = dbStock.IdStock;
                    }
                    else
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensaje = "Stock negativo";
                        return NotFound(responseApi);
                    }
                    

                    
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

        [HttpPut]
        [Route("Agregar/{codigoTienda}/{talle}/{color}")]
        public async Task<ActionResult> AgregarCantidad(int codigoTienda, string talle, string color, Stock stock)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTalle = await _dbContext.Talle.Where(t => t.DescripcionTalle == talle).FirstOrDefaultAsync();
                var dbColor = await _dbContext.ColorArticulo.Where(t => t.DescripcionColor == color).FirstOrDefaultAsync();
                var dbArticulos = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();

                if (dbTalle != null && dbColor != null)
                {
                    var dbStock = await _dbContext.Stock.Where(cant => cant.IdTalle == dbTalle.IdTalle && cant.IdColor == dbColor.IdColor && cant.IdArticulo == dbArticulos.IdCodigo).FirstOrDefaultAsync();

                    if (dbStock == null)
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensaje = "Stock no encontrado";
                        return NotFound(responseApi);
                    }
                    //Cambiar propiedades de stock
                    stock.Cantidad = dbStock.Cantidad + stock.Cantidad;
                    stock.IdSucursal = dbStock.IdSucursal;
                    stock.IdArticulo = dbStock.IdArticulo;
                    stock.IdTalle = dbStock.IdTalle;
                    stock.IdColor = dbStock.IdColor;
                    stock.IdStock = dbStock.IdStock;
                    stock.Articulo = null;
                    stock.Color = null;
                    stock.Talle = null;

                    // Update properties of dbArticulo with values from ArticuloDTO
                    _mapper.Map(stock, dbStock);


                    _dbContext.Entry(dbStock).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbStock.IdStock;
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


        [HttpPost]
        public async Task<ActionResult> AgregarStock(Stock stock)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {

                var dbStock = _mapper.Map<StockDTO>(stock);


                _dbContext.Stock.Add(dbStock);
                await _dbContext.SaveChangesAsync();

                if (dbStock.IdStock != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbStock.IdStock;
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



    }
}
