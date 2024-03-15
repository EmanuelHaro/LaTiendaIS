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
    public class PagoServicio: IPagoServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public PagoServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<Pago> ObtenerConVentaPago(int idVenta)
        {
            try
            {
                var dbPago = await _unitofwork.Repository<PagoDTO>().Obtener(c => c.IdVenta == idVenta).FirstOrDefaultAsync(); 

                if (dbPago == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
                var pago = _mapper.Map<Pago>(dbPago);

                return pago;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
