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
    public class VentaController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public VentaController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarVentas()
        {
            var responseApi = new ResponseAPI<List<VentaDTO>>();
            var listaVentasDTO = new List<VentaDTO>();
            try
            {
                var VentaDb = await _dbContext.Venta.ToListAsync();
                foreach (var Venta in VentaDb)
                {
                    listaVentasDTO.Add(_mapper.Map<VentaDTO>(Venta));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaVentasDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpGet]
        [Route("{idVenta}")]
        public async Task<ActionResult> ObtenerVenta(int idVenta, int idTalle, int idColor)
        {
            var responseApi = new ResponseAPI<VentaDTO>();
            var VentaDTO = new VentaDTO();

            try
            {
                var dbVenta = await _dbContext.Venta.FirstOrDefaultAsync(f => f.IdVenta == idVenta);


                if (dbVenta != null)
                {

                    VentaDTO = _mapper.Map<VentaDTO>(dbVenta);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = VentaDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrado";
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
        public async Task<ActionResult> ObtenerUltimaVenta()
        {
            var responseApi = new ResponseAPI<VentaDTO>();
            var VentaDTO = new VentaDTO();

            try
            {
                var dbVenta = await _dbContext.Venta
                .OrderByDescending(v => v.FechaVenta) // Ordenar las ventas por fecha de venta en orden descendente
                .FirstOrDefaultAsync(); // Tomar la primera venta (la última en la secuencia ordenada) 


                if (dbVenta != null)
                {

                    VentaDTO = _mapper.Map<VentaDTO>(dbVenta);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = VentaDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrado";
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
        public async Task<ActionResult> AgregarVenta(VentaDTO VentaDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbVenta = _mapper.Map<Venta>(VentaDTO);


                _dbContext.Venta.Add(dbVenta);
                await _dbContext.SaveChangesAsync();

                if (dbVenta.IdVenta != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbVenta.IdVenta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al Venta";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut("{IdVenta}")]
        public async Task<ActionResult> ModificarVenta(int IdVenta, VentaDTO VentaDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbVenta = await _dbContext.Venta.Where(c => c.IdVenta == IdVenta).FirstOrDefaultAsync();

                if (dbVenta == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrado";
                    return NotFound(responseApi);
                }

                // Update properties of dbVenta with values from VentaDTO
                _mapper.Map(VentaDTO, dbVenta);


                _dbContext.Entry(dbVenta).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbVenta.IdVenta;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejar la excepción específica de concurrencia aquí
                // Puedes agregar el código necesario para manejar esta situación
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Error de concurrencia al intentar modificar el Venta. No se suministraron las claves primarias correctamente.";
                return StatusCode(500, responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(500, responseApi); // Internal Server Error
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("{IdVenta}")]
        public async Task<IActionResult> EliminarVenta(int IdVenta)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbVenta = await _dbContext.Venta.FirstOrDefaultAsync(f => f.IdVenta == IdVenta);
                if (dbVenta != null)
                {
                    _dbContext.Venta.Remove(dbVenta);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Venta no encontrado";
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

