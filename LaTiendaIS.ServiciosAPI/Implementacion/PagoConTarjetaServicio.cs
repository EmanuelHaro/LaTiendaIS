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
    public class PagoConTarjetaServicio:IPagoConTarjetaServicio
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public PagoConTarjetaServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarPago(PagoConTarjeta Pago)
        {
            try
            {
                var dbPago = _mapper.Map<PagoConTarjetaDTO>(Pago);


                var respModelo = await _unitofwork.Repository<PagoConTarjetaDTO>().Crear(dbPago);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el Pago");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Pago> ObtenerPago(int idPago)
        {
            try
            {
                var dbPago = await _unitofwork.Repository<PagoConTarjetaDTO>().Obtener(c => c.IdPago == idPago).FirstOrDefaultAsync(); 

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
