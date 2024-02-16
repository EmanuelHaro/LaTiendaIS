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
            var responseApi = new ResponseAPI<ClienteDTO>();
            var ClienteDTO = new ClienteDTO();

            try
            {
                var dbCliente = await _dbContext.Cliente.FirstOrDefaultAsync(f => f.IdCliente == IdCliente);


                if (dbCliente != null)
                {
                    dbCliente.CondicionTributaria = _dbContext.CondicionTributaria.Find(dbCliente.IdCondicionTributaria);

                    ClienteDTO = _mapper.Map<ClienteDTO>(dbCliente);

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
        public async Task<ActionResult> AgregarCliente(ClienteDTO ClienteDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbCliente = _mapper.Map<Cliente>(ClienteDTO);


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

        //[HttpGet]
        //[Route("Lista")]
        //public async Task<IActionResult> ListarClientes()
        //{
        //    var responseApi = new ResponseAPI<List<ClienteDTO>>();
        //    var listaClientesDTO = new List<ClienteDTO>();
        //    try
        //    {
        //        var ClienteDb = await _dbContext.Cliente.ToListAsync();
        //        foreach (var Cliente in ClienteDb)
        //        {
        //            Cliente.Marca = _dbContext.Marca.Find(Cliente.IdMarca);
        //            Cliente.Talle = _dbContext.Talle.Find(Cliente.IdTalle);
        //            Cliente.Talle.TipoTalle = _dbContext.TipoTalle.Find(Cliente.Talle.IdTipoTalle);
        //            Cliente.Color = _dbContext.ColorCliente.Find(Cliente.IdColor);
        //            Cliente.Categoria = _dbContext.Categoria.Find(Cliente.IdCategoria);
        //            listaClientesDTO.Add(_mapper.Map<ClienteDTO>(Cliente));
        //        }

        //        responseApi.EsCorrecto = true;
        //        responseApi.Valor = listaClientesDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }

        //    return Ok(responseApi);
        //}

        //[HttpPut("{IdCodigo}")]
        //public async Task<ActionResult> ModificarCliente(int IdCodigo, ClienteDTO ClienteDTO)
        //{
        //    var responseApi = new ResponseAPI<int>();

        //    try
        //    {
        //        var dbCliente = await _dbContext.Cliente.Where(c => c.CodigoTienda == IdCodigo).FirstOrDefaultAsync();

        //        if (dbCliente == null)
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "Cliente no encontrado";
        //            return NotFound(responseApi);
        //        }

        //        // Update properties of dbCliente with values from ClienteDTO
        //        _mapper.Map(ClienteDTO, dbCliente);


        //        _dbContext.Entry(dbCliente).State = EntityState.Modified;
        //        await _dbContext.SaveChangesAsync();

        //        responseApi.EsCorrecto = true;
        //        responseApi.Valor = dbCliente.IdCodigo;
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        // Manejar la excepción específica de concurrencia aquí
        //        // Puedes agregar el código necesario para manejar esta situación
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = "Error de concurrencia al intentar modificar el Cliente. No se suministraron las claves primarias correctamente.";
        //        return StatusCode(500, responseApi);
        //    }
        //    catch (Exception ex)
        //    {
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //        return StatusCode(500, responseApi); // Internal Server Error
        //    }

        //    return Ok(responseApi);
        //}


        //[HttpDelete]
        //[Route("{IdCodigo}")]
        //public async Task<IActionResult> EliminarCliente(int IdCodigo)
        //{
        //    var responseApi = new ResponseAPI<int>();

        //    try
        //    {
        //        var dbCliente = await _dbContext.Cliente.FirstOrDefaultAsync(f => f.IdCodigo == IdCodigo);
        //        if (dbCliente != null)
        //        {
        //            _dbContext.Cliente.Remove(dbCliente);
        //            await _dbContext.SaveChangesAsync();

        //            responseApi.EsCorrecto = true;
        //        }
        //        else
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "Cliente no encontrado";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }

        //    return Ok(responseApi);
        //}

    }
}