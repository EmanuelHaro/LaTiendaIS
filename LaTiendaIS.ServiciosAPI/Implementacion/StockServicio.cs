using AutoMapper;
using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public StockServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<Stock> ObtenerStockPorId(int idStock)
        {
            try
            {
                var dbStock = await _unitofwork.Repository<StockDTO>().Obtener(c => c.IdStock == idStock).FirstOrDefaultAsync();

                if (dbStock == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                var dbTalle = await _unitofwork.Repository<TalleDTO>().Obtener(c => c.IdTalle == dbStock.IdTalle).FirstOrDefaultAsync();
                var dbColor = await _unitofwork.Repository<ColorArticuloDTO>().Obtener(c => c.IdColor == dbStock.IdColor).FirstOrDefaultAsync();
                var dbArticulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.IdCodigo == dbStock.IdArticulo).FirstOrDefaultAsync();
                dbArticulo.Marca = await _unitofwork.Repository<MarcaDTO>().Obtener(m => m.IdMarca == dbArticulo.IdMarca).FirstOrDefaultAsync();
                dbArticulo.Categoria = await _unitofwork.Repository<CategoriaDTO>().Obtener(c => c.IdCategoria == dbArticulo.IdCategoria).FirstOrDefaultAsync();


                var Stock = _mapper.Map<Stock>(dbStock);

                if (dbTalle != null && dbColor != null && dbArticulo != null)
                {
                    Stock.Talle = _mapper.Map<Talle>(dbTalle);
                    Stock.Color = _mapper.Map<ColorArticulo>(dbColor);
                    Stock.Articulo = _mapper.Map<Articulo>(dbArticulo);
                }

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
                var dbTalle = await _unitofwork.Repository<TalleDTO>().Obtener(c => c.DescripcionTalle== talle).FirstOrDefaultAsync();
                var dbColor = await _unitofwork.Repository<ColorArticuloDTO>().Obtener(c => c.DescripcionColor == color).FirstOrDefaultAsync();
                var dbArticulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.CodigoTienda == codigoTienda).FirstOrDefaultAsync();


                if (dbTalle != null && dbColor != null && dbArticulo != null)
                {

                    var dbStock = await _unitofwork.Repository<StockDTO>().Obtener(c => c.IdArticulo == dbArticulo.IdCodigo 
                    && c.IdTalle == dbTalle.IdTalle && c.IdColor == dbColor.IdColor).FirstOrDefaultAsync();

                    if (dbStock != null)
                    {
                        return dbStock.Cantidad;
                    }
                }
                
                throw new TaskCanceledException("No se encontraron resultados");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Stock>> ObtenerListaDeStockPorArticulo(int idArticulo)
        {

            try
            {
                var dbArticulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.CodigoTienda == idArticulo).FirstOrDefaultAsync();
                dbArticulo.Marca = await _unitofwork.Repository<MarcaDTO>().Obtener(m => m.IdMarca == dbArticulo.IdMarca).FirstOrDefaultAsync();
                dbArticulo.Categoria = await _unitofwork.Repository<CategoriaDTO>().Obtener(c => c.IdCategoria == dbArticulo.IdCategoria).FirstOrDefaultAsync();

                var dbStock = await _unitofwork.Repository<StockDTO>().Obtener(c => c.IdArticulo == dbArticulo.IdCodigo && c.Cantidad > 0).ToListAsync();



                if (dbStock == null)
                {
                    throw new TaskCanceledException("No se encontraro el stock para el articulo");
                }

                foreach (var stockItem in dbStock)
                {
                    stockItem.Talle = await _unitofwork.Repository<TalleDTO>().Obtener(c => c.IdTalle == stockItem.IdTalle).FirstOrDefaultAsync();
                    stockItem.Color = await _unitofwork.Repository<ColorArticuloDTO>().Obtener(c => c.IdColor == stockItem.IdColor).FirstOrDefaultAsync();
                    stockItem.Articulo = dbArticulo;
                }

                var stock = _mapper.Map<List<Stock>>(dbStock);

                return stock;

            }
            catch (Exception ex)
            {
                throw new TaskCanceledException("No se encontraro al Articulo");
            }

        }
        public async Task<Stock> ObtenerStockPorArticulo(int codigoTienda, string talle, string color)
        {
            try
            {
                var dbTalle = await _unitofwork.Repository<TalleDTO>().Obtener(c => c.DescripcionTalle == talle).FirstOrDefaultAsync();
                var dbColor = await _unitofwork.Repository<ColorArticuloDTO>().Obtener(c => c.DescripcionColor == color).FirstOrDefaultAsync();
                var dbArticulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.CodigoTienda == codigoTienda).FirstOrDefaultAsync();

                dbArticulo.Marca = await _unitofwork.Repository<MarcaDTO>().Obtener(m => m.IdMarca == dbArticulo.IdMarca).FirstOrDefaultAsync();
                dbArticulo.Categoria = await _unitofwork.Repository<CategoriaDTO>().Obtener(c => c.IdCategoria == dbArticulo.IdCategoria).FirstOrDefaultAsync();


                if (dbTalle != null && dbColor != null && dbArticulo != null)
                {
                    var dbStock = await _unitofwork.Repository<StockDTO>().Obtener(c => c.IdArticulo == dbArticulo.IdCodigo
                    && c.IdTalle == dbTalle.IdTalle && c.IdColor == dbColor.IdColor).FirstOrDefaultAsync();

                    if (dbStock == null)
                    {
                        throw new TaskCanceledException("No se encontraron resultados");
                    }
                    dbStock.Talle = dbTalle;
                    dbStock.Color = dbColor;
                    dbStock.Articulo = dbArticulo;

                    var Stock = _mapper.Map<Stock>(dbStock);

                    return Stock;
                }

                throw new TaskCanceledException("No se encontraron articulo,talle,color");

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
                var dbTalle = await _unitofwork.Repository<TalleDTO>().Obtener(c => c.DescripcionTalle == talle).FirstOrDefaultAsync();
                var dbColor = await _unitofwork.Repository<ColorArticuloDTO>().Obtener(c => c.DescripcionColor == color).FirstOrDefaultAsync();
                var dbArticulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.CodigoTienda == codigoTienda).FirstOrDefaultAsync();

                if (dbTalle != null && dbColor != null && dbArticulo != null)
                {
                    var dbStock = await _unitofwork.Repository<StockDTO>().Obtener(c => c.IdArticulo == dbArticulo.IdCodigo
                    && c.IdTalle == dbTalle.IdTalle && c.IdColor == dbColor.IdColor).FirstOrDefaultAsync();

                    if (dbStock == null)
                    {
                        throw new TaskCanceledException("No se encontraron resultados");
                    }

                    // actualizo cantidad
                    if (dbStock.Cantidad - stock.Cantidad >= 0)
                    {

                        var stockUpdate = new StockUpdateDto { Cantidad = dbStock.Cantidad - stock.Cantidad };
                        _mapper.Map(stockUpdate, dbStock);



                        _mapper.Map(stockUpdate, dbStock);

                        var respuesta = await _unitofwork.Repository<StockDTO>().Modificar(dbStock);
                        if (!respuesta)
                            throw new TaskCanceledException("No se pudo editar");

                        return respuesta;
                    }

                }

                throw new TaskCanceledException("No se encontro articulo,talle,color");

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
                var dbTalle = await _unitofwork.Repository<TalleDTO>().Obtener(c => c.DescripcionTalle == talle).FirstOrDefaultAsync();
                var dbColor = await _unitofwork.Repository<ColorArticuloDTO>().Obtener(c => c.DescripcionColor == color).FirstOrDefaultAsync();
                var dbArticulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.CodigoTienda == codigoTienda).FirstOrDefaultAsync();

                if (dbTalle != null && dbColor != null && dbArticulo != null)
                {
                    var dbStock = await _unitofwork.Repository<StockDTO>().Obtener(c => c.IdArticulo == dbArticulo.IdCodigo
                    && c.IdTalle == dbTalle.IdTalle && c.IdColor == dbColor.IdColor).FirstOrDefaultAsync();

                    if (dbStock == null)
                    {
                        throw new TaskCanceledException("No se encontraron resultados");
                    }

                    // actualizo Cantidad
                   
                    var stockUpdate = new StockUpdateDto { Cantidad = dbStock.Cantidad + stock.Cantidad };
                    _mapper.Map(stockUpdate, dbStock);



                    _mapper.Map(stockUpdate, dbStock);

                    var respuesta = await _unitofwork.Repository<StockDTO>().Modificar(dbStock);
                    if (!respuesta)
                        throw new TaskCanceledException("No se pudo editar");

                    return respuesta;
                }

                throw new TaskCanceledException("No se encontro articulo,talle,color");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Stock>> ListarStock()
        {
            try
            {
                var dbStock = await _unitofwork.Repository<StockDTO>().Obtener().ToListAsync();

                if (dbStock == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                var Stock = _mapper.Map<List<Stock>>(dbStock);

                return Stock;

            }
            catch (Exception ex)
            {
                throw;
            }
        } //PARA TESTING

        public async Task<bool> AgregarStock(Stock stock) //PARA TESTING
        {
            try
            {
                var dbStock = _mapper.Map<StockDTO>(stock);


                var respModelo = await _unitofwork.Repository<StockDTO>().Crear(dbStock);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el articulo");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
