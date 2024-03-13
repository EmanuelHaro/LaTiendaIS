using AutoMapper;
using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.Shared.Models;
using LaTiendaIS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaTiendaIS.ServiciosAPI.Contrato;
using Microsoft.EntityFrameworkCore;

namespace LaTiendaIS.ServiciosAPI.Implementacion
{
    public class SucursalServicio : ISucursalServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public SucursalServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarSucursal(Sucursal Sucursal)
        {
            try
            {
                var dbSucursal = _mapper.Map<SucursalDTO>(Sucursal);

                var respModelo = await _unitofwork.Repository<SucursalDTO>().Crear(dbSucursal);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el Sucursal");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Sucursal> ObtenerSucursal(string descSucursal)
        {
            try
            {
                var dbSucursal = await _unitofwork.Repository<SucursalDTO>().Obtener(c => c.Nombre == descSucursal).FirstOrDefaultAsync();
                if (dbSucursal == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
                var art = _mapper.Map<Sucursal>(dbSucursal);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
