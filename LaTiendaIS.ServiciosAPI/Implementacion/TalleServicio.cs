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
    public class TalleServicio : ITalleServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public TalleServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarTalle(Talle Talle)
        {
            try
            {
                var dbTalle = _mapper.Map<TalleDTO>(Talle);

                var respModelo = await _unitofwork.Repository<TalleDTO>().Crear(dbTalle);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el Talle");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Talle> ObtenerTalle(string descTalle)
        {
            try
            {
                var dbTalle = await _unitofwork.Repository<TalleDTO>().Obtener(c => c.DescripcionTalle == descTalle).FirstOrDefaultAsync();
                if (dbTalle == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
                var art = _mapper.Map<Talle>(dbTalle);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
