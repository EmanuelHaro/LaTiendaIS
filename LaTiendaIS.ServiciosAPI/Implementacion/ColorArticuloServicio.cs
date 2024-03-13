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
    public class ColorArticuloServicio : IColorArticuloServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public ColorArticuloServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarColorArticulo(ColorArticulo ColorArticulo)
        {
            try
            {
                var dbColorArticulo = _mapper.Map<ColorArticuloDTO>(ColorArticulo);

                var respModelo = await _unitofwork.Repository<ColorArticuloDTO>().Crear(dbColorArticulo);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el ColorArticulo");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ColorArticulo> ObtenerColorArticulo(string descColorArticulo)
        {
            try
            {
                var dbColorArticulo = await _unitofwork.Repository<ColorArticuloDTO>().Obtener(c => c.DescripcionColor == descColorArticulo).FirstOrDefaultAsync();
                if (dbColorArticulo == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
                var art = _mapper.Map<ColorArticulo>(dbColorArticulo);

                return art;

            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
