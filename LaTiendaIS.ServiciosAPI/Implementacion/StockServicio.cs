using AutoMapper;
using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Implementacion
{
    public class StockServicio: IStockServicio
    {
        private readonly IGenericoRepositorio<StockDTO> _modeloRepositorio;
        private readonly IMapper _mapper;

        public StockServicio(IGenericoRepositorio<StockDTO> modeloRepositorio, IMapper mapper)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }

        public async Task<Stock> ObtenerStockPorId(int idStock)
        {
            try
            {
                var dbStock = await _modeloRepositorio.Obtener(c => c.IdStock == idStock).FirstOrDefaultAsync();

                if (dbStock == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                //TODO: STOCK SERVICIO - OBTENERSTOCKPORID
                //var dbTalle = await _dbContext.Talle.Where(t => t.IdTalle == dbStock.IdTalle).FirstOrDefaultAsync();
                //var dbColor = await _dbContext.ColorArticulo.Where(t => t.IdColor == dbStock.IdColor).FirstOrDefaultAsync();
                //var dbArticulo = await _dbContext.Articulo.Where(a => a.IdCodigo == dbStock.IdArticulo).FirstOrDefaultAsync();

                //if (dbTalle != null && dbColor != null && dbArticulo != null)
                //{
                //    stock.Talle = _mapper.Map<Talle>(dbTalle);
                //    stock.Color = _mapper.Map<ColorArticulo>(dbColor);
                //    stock.Articulo = _mapper.Map<Articulo>(dbArticulo);

                var Stock = _mapper.Map<Stock>(dbStock);

                return Stock;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<int> ObtenerCantidad(int codigoTienda, string talle, string color)
        {
            try
            {
                //TODO: STOCK SERVICIO - OBTENER CANTIDAD
                //var dbTalle = await _dbContext.Talle.Where(t => t.DescripcionTalle == talle).FirstOrDefaultAsync();
                //var dbColor = await _dbContext.ColorArticulo.Where(t => t.DescripcionColor == color).FirstOrDefaultAsync();
                //var dbArticulo = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();
                //if (dbTalle != null && dbColor != null)
                //{

                var dbStock = await _modeloRepositorio.Obtener(c => c.IdArticulo == 1 && c.IdTalle == 1 && c.IdColor == 1).FirstOrDefaultAsync();

                if (dbStock == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                return dbStock.Cantidad;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Stock>> ObtenerListaDeStockPorArticulo(int idArticulo)
        {
            //TODO: STOCKSERVICIO - OBTENERLISTADETALLEYCOLORDELSTOCK
            //var dbArticulo = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();
            //if (dbArticulo != null)
            //{

            try
            {
                //hardcodeado porque no tenemos codigoTienda
                var dbStock = await _modeloRepositorio.Obtener(c => c.IdArticulo == 1 && a.Cantidad > 0).ToListAsync();

                if (dbStock == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                //foreach (var stockItem in dbStock)
                //{
                //    stockItem.Talle = _dbContext.Talle.Find(stockItem.IdTalle);
                //    stockItem.Color = _dbContext.ColorArticulo.Find(stockItem.IdColor);
                //    stockItem.Articulo = _dbContext.Articulo.Find(stockItem.IdArticulo);
                //}

                var stock = _mapper.Map<List<Stock>>(dbStock);

                return stock;

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Stock> ObtenerStockPorArticulo(int codigoTienda, string talle, string color)
        {
            try
            {
                //TODO: STOCK SERVICIO - ObtenerStockPorArticulo
                //var dbTalle = await _dbContext.Talle.Where(t => t.DescripcionTalle == talle).FirstOrDefaultAsync();
                //var dbColor = await _dbContext.ColorArticulo.Where(t => t.DescripcionColor == color).FirstOrDefaultAsync();
                //var dbArticulo = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();
                //if (dbTalle != null && dbColor != null)
                //{

                var dbStock = await _modeloRepositorio.Obtener(c => c.IdArticulo == 1 && c.IdTalle == 1 && c.IdColor == 1).FirstOrDefaultAsync();

                if (dbStock == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                var Stock = _mapper.Map<Stock>(dbStock);

                return Stock;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> ModificarCantidad(int codigoTienda, string talle, string color, Stock stock)
        {
            try
            {
                //TODO: STOCK SERVICIO - ModificarCantidad
                //var dbTalle = await _dbContext.Talle.Where(t => t.DescripcionTalle == talle).FirstOrDefaultAsync();
                //var dbColor = await _dbContext.ColorArticulo.Where(t => t.DescripcionColor == color).FirstOrDefaultAsync();
                //var dbArticulo = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();
                //if (dbTalle != null && dbColor != null)
                //{

                var dbStock = await _modeloRepositorio.Obtener(c => c.IdArticulo == 1 && c.IdTalle == 1 && c.IdColor == 1).FirstOrDefaultAsync();

                if (dbStock == null)
                    throw new TaskCanceledException("No se encontro al articulo");

                // actualizo propiedades
                //Cambiar propiedades de stock
                if (dbStock.Cantidad - stock.Cantidad >= 0)
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
                }

                _mapper.Map(stock, dbStock);

                var respuesta = await _modeloRepositorio.Modificar(dbStock);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");
                return respuesta;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> AgregarCantidadStock(int codigoTienda, string talle, string color, Stock stock)
        {
            try
            {
                //TODO: STOCK SERVICIO - AgregarCantidadStock
                //var dbTalle = await _dbContext.Talle.Where(t => t.DescripcionTalle == talle).FirstOrDefaultAsync();
                //var dbColor = await _dbContext.ColorArticulo.Where(t => t.DescripcionColor == color).FirstOrDefaultAsync();
                //var dbArticulo = await _dbContext.Articulo.Where(a => a.CodigoTienda == codigoTienda).FirstOrDefaultAsync();
                //if (dbTalle != null && dbColor != null)
                //{

                var dbStock = await _modeloRepositorio.Obtener(c => c.IdArticulo == 1 && c.IdTalle == 1 && c.IdColor == 1).FirstOrDefaultAsync();

                if (dbStock == null)
                    throw new TaskCanceledException("No se encontro al articulo");

                // actualizo propiedades
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
                

                _mapper.Map(stock, dbStock);

                var respuesta = await _modeloRepositorio.Modificar(dbStock);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");
                return respuesta;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
