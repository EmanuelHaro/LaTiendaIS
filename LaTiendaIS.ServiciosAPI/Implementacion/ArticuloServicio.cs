using AutoMapper;
using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Implementacion
{
    public class ArticuloServicio : IArticuloServicio
    {
        private readonly IUnitOfWork _unitofwork;
       // private readonly IGenericoRepositorio<ArticuloDTO> _modeloRepositorio;
        private readonly IMapper _mapper;

        public ArticuloServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<List<Articulo>> ListarArticulos()
        {
            try
            {
                List<ArticuloDTO> listaArticulos = new List<ArticuloDTO>();
                var listaDB = await _unitofwork.Repository<ArticuloDTO>().Obtener().ToListAsync();

                foreach (var Articulo in listaDB)
                {
                    Articulo.Marca = await _unitofwork.Repository<MarcaDTO>().Obtener(c => c.IdMarca == Articulo.IdMarca).FirstOrDefaultAsync();
                    Articulo.Categoria = await _unitofwork.Repository<CategoriaDTO>().Obtener(c => c.IdCategoria == Articulo.IdCategoria).FirstOrDefaultAsync();
                    listaArticulos.Add(Articulo);
                }

                List<Articulo> lista = _mapper.Map<List<Articulo>>(listaArticulos);
                return lista;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Articulo> ObtenerArticulo(int idArticulo)
        {
            try
            {
                var dbArticulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.CodigoTienda == idArticulo).FirstOrDefaultAsync(); //IGenericoRepositio<ArticuloDTO>.Obtener()  _modeloRepositorio.Obtener()

                if (dbArticulo == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                dbArticulo.Marca = await _unitofwork.Repository<MarcaDTO>().Obtener(c => c.IdMarca == dbArticulo.IdMarca).FirstOrDefaultAsync();

                dbArticulo.Categoria = await _unitofwork.Repository<CategoriaDTO>().Obtener(c => c.IdCategoria == dbArticulo.IdCategoria).FirstOrDefaultAsync();

                var art = _mapper.Map<Articulo>(dbArticulo);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public async Task<bool> AgregarArticulo(Articulo articulo)
        {
            try
            {
                var dbArticulo = _mapper.Map<ArticuloDTO>(articulo);


                var respModelo = await _unitofwork.Repository<ArticuloDTO>().Crear(dbArticulo);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el articulo");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ModificarArticulo(int IdCodigo, Articulo articulo)
        {
            try
            {
                var dbArticulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.CodigoTienda == IdCodigo).FirstOrDefaultAsync();

                if (dbArticulo == null)
                    throw new TaskCanceledException("No se encontro al articulo");

                // actualizo propiedades
                _mapper.Map(articulo, dbArticulo);

                var respuesta = await _unitofwork.Repository<ArticuloDTO>().Modificar(dbArticulo);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");
                return respuesta;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EliminarArticulo(int idArticulo)
        {
            try
            {
                var dbrticulo = _unitofwork.Repository<ArticuloDTO>().Obtener(p => p.IdCodigo == idArticulo); //devuelve un IQueryable
                var fromDbModelo = await dbrticulo.FirstOrDefaultAsync(); // a revisar

                if (fromDbModelo != null)
                {
                    var respuesta = await _unitofwork.Repository<ArticuloDTO>().Eliminar(fromDbModelo);
                    if (!respuesta)
                        throw new TaskCanceledException("No se pudo eliminar");
                    return respuesta;
                }
                else
                    throw new TaskCanceledException("No se encontraron resultados");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
