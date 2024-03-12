using AutoMapper;
using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Implementacion
{
    public class CondicionTServicio: ICondicionTServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public CondicionTServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<CondicionTributaria> ObtenerCondicionTributaria(string descCondicion)
        {
            try
            {
                var dbCondicionT = await _unitofwork.Repository<CondicionTributariaDTO>().Obtener(c => c.Descripcion == descCondicion).FirstOrDefaultAsync();

                if (dbCondicionT == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
                var art = _mapper.Map<CondicionTributaria>(dbCondicionT);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
