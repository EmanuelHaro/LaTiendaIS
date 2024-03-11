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
    public class CondicionTServicio: ICondicionTServicio
    {
        private readonly IGenericoRepositorio<CondicionTributariaDTO> _modeloRepositorio;
        private readonly IMapper _mapper;

        public CondicionTServicio(IGenericoRepositorio<CondicionTributariaDTO> modeloRepositorio, IMapper mapper)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }

        public async Task<CondicionTributaria> ObtenerCondicionTributaria(string descCondicion)
        {
            try
            {
                var dbCondicionT = await _modeloRepositorio.Obtener(c => c.Descripcion == descCondicion).FirstOrDefaultAsync();

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
