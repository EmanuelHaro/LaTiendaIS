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
    public class StockController : ControllerBase
    {
        private readonly IStockServicio _StockServicio;

        public StockController(IStockServicio StockServicio)
        {
            _StockServicio = StockServicio;
        }

        [HttpGet] //Realizado para testing
        public async Task<IActionResult> ListarStock()
        {
            var responseApi = new ResponseAPI<List<Stock>>();

            try
            {
                var lista = await _StockServicio.ListarStock();
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

            return Ok(responseApi.Valor);
        }



        [HttpGet]
        [Route("Articulo/{codigoTienda}")] 
        public async Task<ActionResult> ObtenerListaDeStockPorArticulo(int codigoTienda)
        {
            var responseApi = new ResponseAPI<List<Stock>>();
            var Stock = new List<Stock>();

            try
            {
                var listaStock = await _StockServicio.ObtenerListaDeStockPorArticulo(codigoTienda);

                if (listaStock != null)
                {

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Stock;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Articulo no encontrado en Stock/Cantidad insuficiente";
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
        [Route("Cantidad/{codigoTienda}/{talle}/{color}")]
        public async Task<ActionResult> ObtenerCantidad(int codigoTienda,string talle, string color)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var cant = await _StockServicio.ObtenerCantidad(codigoTienda,talle,color); 

                if (cant != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = cant;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No coinciden talle y color";
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
        public async Task<ActionResult> ObtenerStockPorArticulo(int codigoTienda, string talle, string color)
        {
            var responseApi = new ResponseAPI<Stock>();

            try
            {
                var stock = await _StockServicio.ObtenerStockPorArticulo(codigoTienda,talle,color);

                if (stock != null)
                {

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = stock;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No coinciden talle y color";
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
        [Route("{idStock}")] //localhost:5020/api/Stock/1
        public async Task<ActionResult> ObtenerStockPorId(int idStock)
        {
            var responseApi = new ResponseAPI<Stock>();

            try
            {
                var stock = await _StockServicio.ObtenerStockPorId(idStock);

                if (stock != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = stock;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No coinciden talle y color";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi.Valor);
        }

        [HttpPut]
        [Route("Modificar/{codigoTienda}/{talle}/{color}")]
        public async Task<ActionResult> ModificarCantidad(int codigoTienda, string talle, string color, Stock stock)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var cantidadModificada = await _StockServicio.ModificarCantidad(codigoTienda, talle, color,stock);

                if (!cantidadModificada)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Stock no encontrado";
                    return NotFound(responseApi);
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = cantidadModificada;

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
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var cantidadAgregada = await _StockServicio.AgregarCantidadStock(codigoTienda, talle, color, stock);

                if (!cantidadAgregada)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Stock no encontrado";
                    return NotFound(responseApi);
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = cantidadAgregada;

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
        public async Task<ActionResult> AgregarStock(Stock stock) //ME QUEDE EN ESTA FUNCION
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
