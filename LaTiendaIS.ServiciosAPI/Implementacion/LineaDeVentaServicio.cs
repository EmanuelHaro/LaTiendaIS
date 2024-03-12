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
    public class LineaDeVentaServicio: ILineaDeVentaServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public LineaDeVentaServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarLineaDeVenta(LineaDeVenta LineaDeVenta)
        {
            try
            {
                var dbLDV = _mapper.Map<LineaDeVentaDTO>(LineaDeVenta);

                var respModelo = await _unitofwork.Repository<LineaDeVentaDTO>().Crear(dbLDV);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el LineaDeVenta");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EliminarLineaDeVenta(int idLineaDeVenta)
        {
            try
            {
                var dbrticulo = _unitofwork.Repository<LineaDeVentaDTO>().Obtener(p => p.IdLineaDeVenta == idLineaDeVenta); //devuelve un IQueryable
                var fromDbModelo = await dbrticulo.FirstOrDefaultAsync(); // a revisar

                if (fromDbModelo != null)
                {
                    var respuesta = await _unitofwork.Repository<LineaDeVentaDTO>().Eliminar(fromDbModelo);
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

        public async Task<List<LineaDeVenta>> ListarLineaDeVentas()
        {
            try
            {
                List<LineaDeVentaDTO> listaDeLDV = new List<LineaDeVentaDTO> ();

                var listaDB = await _unitofwork.Repository<LineaDeVentaDTO>().Obtener().ToListAsync();

                foreach (var LineaDeVenta in listaDB)
                {
                    LineaDeVenta.Articulo = await _unitofwork.Repository<ArticuloDTO>().Obtener(c => c.IdCodigo == LineaDeVenta.IdArticulo).FirstOrDefaultAsync();
                    listaDeLDV.Add(LineaDeVenta);
                }


                List<LineaDeVenta> lista = _mapper.Map<List<LineaDeVenta>>(listaDeLDV);
                return lista;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ModificarLineaDeVenta(int idLineaDeVenta, LineaDeVenta LineaDeVenta)
        {
            try
            {
                var dbLineaDeVenta = await _unitofwork.Repository<LineaDeVentaDTO>().Obtener(c => c.IdLineaDeVenta == idLineaDeVenta).FirstOrDefaultAsync();

                if (dbLineaDeVenta == null)
                    throw new TaskCanceledException("No se encontro la LineaDeVenta");

                // actualizo propiedades
                _mapper.Map(LineaDeVenta, dbLineaDeVenta);

                var respuesta = await _unitofwork.Repository<LineaDeVentaDTO>().Modificar(dbLineaDeVenta);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");
                return respuesta;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LineaDeVenta> ObtenerLineaDeVenta(int idLineaDeVenta)
        {
            try
            {
                var dbLineaDeVenta = await _unitofwork.Repository<LineaDeVentaDTO>().Obtener(c => c.IdLineaDeVenta == idLineaDeVenta).FirstOrDefaultAsync();

                if (dbLineaDeVenta == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                var art = _mapper.Map<LineaDeVenta>(dbLineaDeVenta);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LineaDeVenta> ObtenerUltimaLineaDeVenta()
        {
            try
            {
                var listaLineaDeVenta = await _unitofwork.Repository<LineaDeVentaDTO>().Obtener().ToListAsync();


                if (listaLineaDeVenta == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                
                var LineaDeVenta = listaLineaDeVenta.OrderByDescending(c => c.IdLineaDeVenta);
                var LineaDeVenta1 = _mapper.Map<LineaDeVenta>(LineaDeVenta);

                return LineaDeVenta1;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
