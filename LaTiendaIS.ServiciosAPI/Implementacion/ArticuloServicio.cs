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
        private readonly IGenericoRepositorio<ArticuloDTO> _modeloRepositorio;
        private readonly IMapper _mapper;

        public ArticuloServicio(IGenericoRepositorio<ArticuloDTO> modeloRepositorio, IMapper mapper)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }

        public async Task<List<Articulo>> ListarArticulos()
        {
            try
            {
                var listaDB = await _modeloRepositorio.Obtener().ToListAsync();

                List<Articulo> lista = _mapper.Map<List<Articulo>>(listaDB);
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
                var dbArticulo = await _modeloRepositorio.Obtener(c => c.CodigoTienda == idArticulo).FirstOrDefaultAsync();

                if (dbArticulo == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                //IMPLEMENTAR CUANDO TENGAMOS MARCA Y CATEGORIA IMPLEMENTADO
                //dbArticulo.Marca = _dbContext.Marca.Find(dbArticulo.IdMarca);
                //dbArticulo.Categoria = _dbContext.Categoria.Find(dbArticulo.IdCategoria);

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


                var respModelo = await _modeloRepositorio.Crear(dbArticulo);

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
                var dbArticulo = await _modeloRepositorio.Obtener(c => c.CodigoTienda == IdCodigo).FirstOrDefaultAsync();

                if (dbArticulo == null)
                    throw new TaskCanceledException("No se encontro al articulo");

                // actualizo propiedades
                _mapper.Map(articulo, dbArticulo);

                var respuesta = await _modeloRepositorio.Modificar(dbArticulo);

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
                var dbrticulo = _modeloRepositorio.Obtener(p => p.IdCodigo == idArticulo); //devuelve un IQueryable
                var fromDbModelo = await dbrticulo.FirstOrDefaultAsync(); // a revisar

                if (fromDbModelo != null)
                {
                    var respuesta = await _modeloRepositorio.Eliminar(fromDbModelo);
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
