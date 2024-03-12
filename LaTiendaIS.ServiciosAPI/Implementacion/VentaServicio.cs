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
    public class VentaServicio: IVentaServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public VentaServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarVenta(Venta Venta)
        {
            try
            {
                var dbVenta = _mapper.Map<VentaDTO>(Venta);


                var respModelo = await _unitofwork.Repository<VentaDTO>().Crear(dbVenta);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el Venta");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EliminarVenta(int idVenta)
        {
            try
            {
                var dbventa= _unitofwork.Repository<VentaDTO>().Obtener(p => p.IdVenta == idVenta); //devuelve un IQueryable
                var fromDbModelo = await dbventa.FirstOrDefaultAsync(); // a revisar

                if (fromDbModelo != null)
                {
                    var respuesta = await _unitofwork.Repository<VentaDTO>().Eliminar(fromDbModelo);
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

        public async Task<List<Venta>> ListarVentas()
        {
            try
            {
                var listaDB = await _unitofwork.Repository<VentaDTO>().Obtener().ToListAsync();

                List<Venta> lista = _mapper.Map<List<Venta>>(listaDB);
                return lista;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ModificarVenta(int idVenta, Venta Venta)
        {
            try
            {
                var dbVenta = await _unitofwork.Repository<VentaDTO>().Obtener(c => c.IdVenta == idVenta).FirstOrDefaultAsync();

                if (dbVenta == null)
                    throw new TaskCanceledException("No se encontro al Venta");

                // actualizo propiedades
                _mapper.Map(Venta, dbVenta);

                var respuesta = await _unitofwork.Repository<VentaDTO>().Modificar(dbVenta);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");
                return respuesta;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Venta> ObtenerUltimaVenta()
        {
            try
            {
                var listaVenta = await _unitofwork.Repository<VentaDTO>().Obtener().ToListAsync();


                if (listaVenta == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
                var Venta = listaVenta.OrderByDescending(c => c.IdVenta);
                var Venta1 = _mapper.Map<Venta>(Venta);

                return Venta1;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Venta> ObtenerVenta(int idVenta)
        {
            try
            {
                var dbVenta = await _unitofwork.Repository<VentaDTO>().Obtener(c => c.IdVenta == idVenta).FirstOrDefaultAsync();

                if (dbVenta == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                var art = _mapper.Map<Venta>(dbVenta);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
