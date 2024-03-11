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
        private readonly IGenericoRepositorio<ClienteDTO> _modeloRepositorio;
        private readonly IMapper _mapper;

        public ClienteServicio(IGenericoRepositorio<ClienteDTO> modeloRepositorio, IMapper mapper)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }

        public async Task<bool> AgregarCliente(Cliente Cliente)
        {
            try
            {
                var dbCliente = _mapper.Map<ClienteDTO>(Cliente);


                var respModelo = await _modeloRepositorio.Crear(dbCliente);

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
                var dbcliente = await _modeloRepositorio.Obtener(c => c.IdCliente == idCliente).FirstOrDefaultAsync();

                if (dbcliente == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                //TODO: CLIENTE SERVICIO
                //dbCliente.CondicionTributaria = _dbContext.CondicionTributaria.Find(dbCliente.IdCondicionTributaria);
                var art = _mapper.Map<Cliente>(dbcliente);

                return art;

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
                var listaCliente = await _modeloRepositorio.Obtener().ToListAsync();
                

                if (listaCliente == null)
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

                //TODO: CLIENTE SERVICIO 
                //dbCliente.CondicionTributaria = _dbContext.CondicionTributaria.Find(dbCliente.IdCondicionTributaria);
                var cliente = listaCliente.OrderByDescending(c => c.IdCliente);
                var cliente1 = _mapper.Map<Cliente>(cliente);

                return cliente1;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
