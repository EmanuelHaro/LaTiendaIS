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
    public class MarcaServicio : IMarcaServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public MarcaServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarMarca(Marca Marca)
        {
            try
            {
                var dbMarca = _mapper.Map<MarcaDTO>(Marca);

                var respModelo = await _unitofwork.Repository<MarcaDTO>().Crear(dbMarca);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el Marca");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Marca> ObtenerMarca(string descMarca)
        {
            try
            {
                var dbMarca = await _unitofwork.Repository<MarcaDTO>().Obtener(c => c.DescripcionMarca == descMarca).FirstOrDefaultAsync();
                if (dbMarca == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
                var art = _mapper.Map<Marca>(dbMarca);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
