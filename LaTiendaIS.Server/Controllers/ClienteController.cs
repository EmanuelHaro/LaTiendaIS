using AutoMapper;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public ClienteController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{IdCliente}")]
        public async Task<ActionResult> ObtenerCliente(int IdCliente)
        {
            var responseApi = new ResponseAPI<Cliente>();
            var ClienteDTO = new Cliente();

            try
            {
                var dbCliente = await _dbContext.Cliente.FirstOrDefaultAsync(f => f.IdCliente == IdCliente);


                if (dbCliente != null)
                {
                    dbCliente.CondicionTributaria = _dbContext.CondicionTributaria.Find(dbCliente.IdCondicionTributaria);

                    ClienteDTO = _mapper.Map<Cliente>(dbCliente);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ClienteDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Cliente no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        public async Task<ActionResult> AgregarCliente(Cliente cliente)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbCliente = _mapper.Map<ClienteDTO>(cliente);


                _dbContext.Cliente.Add(dbCliente);
                await _dbContext.SaveChangesAsync();

                if (dbCliente.IdCliente != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbCliente.IdCliente;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al Cliente";
                } 
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Ultima")]
        public async Task<ActionResult> ObtenerUltimaCliente()
        {
            var responseApi = new ResponseAPI<Cliente>();
            var ClienteDTO = new Cliente();

            try
            {
                var dbCliente = await _dbContext.Cliente
                .OrderByDescending(c => c.IdCliente)
                .FirstOrDefaultAsync();

                if (dbCliente != null)
                {

                    dbCliente.CondicionTributaria = _dbContext.CondicionTributaria.Find(dbCliente.IdCondicionTributaria);

                    ClienteDTO = _mapper.Map<Cliente>(dbCliente);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ClienteDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Cliente no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

    }
}