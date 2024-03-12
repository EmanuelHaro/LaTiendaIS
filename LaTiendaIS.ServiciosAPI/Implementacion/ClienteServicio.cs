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
    public class ClienteServicio: IClienteServicio
    {
        private readonly IUnitOfWork _unitofwork;
        //private readonly IGenericoRepositorio<ClienteDTO> _modeloRepositorio;
        private readonly IMapper _mapper;

        public ClienteServicio(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<bool> AgregarCliente(Cliente Cliente)
        {
            try
            {
                var dbCliente = _mapper.Map<ClienteDTO>(Cliente);


                var respModelo = await _unitofwork.Repository<ClienteDTO>().Crear(dbCliente);

                if (!respModelo)
                    throw new TaskCanceledException("No se pudo agregar el Cliente");

                return respModelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Cliente> ObtenerCliente(int idCliente)
        {
            try
            {
                var dbcliente = await _unitofwork.Repository<ClienteDTO>().Obtener(c => c.IdCliente == idCliente).FirstOrDefaultAsync();

                if (dbcliente == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }


                dbcliente.CondicionTributaria = await _unitofwork.Repository<CondicionTributariaDTO>().Obtener(c => c.IdCondicionTributaria == dbcliente.IdCondicionTributaria).FirstOrDefaultAsync();
                var cliente = _mapper.Map<Cliente>(dbcliente);

                return cliente;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Cliente> ObtenerUltimaCliente()
        {
            try
            {
                var listaCliente = await _unitofwork.Repository<ClienteDTO>().Obtener().ToListAsync();
                

                if (listaCliente == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                var dbcliente = listaCliente.OrderByDescending(c => c.IdCliente).FirstOrDefault();

                dbcliente.CondicionTributaria = await _unitofwork.Repository<CondicionTributariaDTO>().Obtener(c => c.IdCondicionTributaria == dbcliente.IdCondicionTributaria).FirstOrDefaultAsync();


                var cliente = _mapper.Map<Cliente>(dbcliente);

                return cliente;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
