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
    public class TipoDeComprobanteServicio : ITipoDeComprobanteServicio
    {
        private readonly IUnitOfWork _unitofwork;

        private readonly IMapper _mapper;

        public TipoDeComprobanteServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<TipoDeComprobante> ObtenerComprobanteConCondicionTributaria(string descCondicion)
        {
            TipoDeComprobanteDTO tdc = null;

            switch (descCondicion)
            {
                case "Responsable Inscripto":
                    tdc = await _unitofwork.Repository<TipoDeComprobanteDTO>().Obtener(c => c.Descripcion == "Factura A").FirstOrDefaultAsync();
                    break;

                case "Monotributo":
                    tdc = await _unitofwork.Repository<TipoDeComprobanteDTO>().Obtener(c => c.Descripcion == "Factura A").FirstOrDefaultAsync();
                    break;

                default:
                    tdc = await _unitofwork.Repository<TipoDeComprobanteDTO>().Obtener(c => c.Descripcion == "Factura B").FirstOrDefaultAsync();
                    break;
            }

            if (tdc == null || tdc.Descripcion == null)
            {
                throw new TaskCanceledException("No se encontraron resultados");
            }

            var comp = _mapper.Map<TipoDeComprobante>(tdc);
            return comp;
        }
    }
}
