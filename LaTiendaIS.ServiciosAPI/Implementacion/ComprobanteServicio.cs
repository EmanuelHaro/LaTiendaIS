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
    public class ComprobanteServicio: IComprobanteServicio
    {
        private readonly IGenericoRepositorio<ComprobanteDTO> _modeloRepositorio;
        private readonly IMapper _mapper;

        public ComprobanteServicio(IGenericoRepositorio<ComprobanteDTO> modeloRepositorio, IMapper mapper)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }

        public async Task<bool> AgregarComprobante(Comprobante Comprobante)
        {
            try
            {
                var dbComprobante = _mapper.Map<ComprobanteDTO>(Comprobante);


                var respModelo = await _modeloRepositorio.Crear(dbComprobante);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el Comprobante");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Comprobante> ObtenerComprobante(int idComprobante)
        {
            try
            {
                var dbComp = await _modeloRepositorio.Obtener(c => c.IdComprobante == idComprobante).FirstOrDefaultAsync();

                if (dbComp == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }


                var comp = _mapper.Map<Comprobante>(dbComp);

                return comp;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
