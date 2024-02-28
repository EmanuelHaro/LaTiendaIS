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
    public class LineaDeVentaController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public LineaDeVentaController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarLineaDeVentas()
        {
            var responseApi = new ResponseAPI<List<LineaDeVenta>>();
            var listaLineaDeVentasDTO = new List<LineaDeVenta>();
            try
            {
                var LineaDeVentaDb = await _dbContext.LineaDeVenta.ToListAsync();
                foreach (var LineaDeVenta in LineaDeVentaDb)
                {
                    //Articulo.Marca = _dbContext.Marca.Find(Articulo.IdMarca);
                    //Articulo.Talle = _dbContext.Talle.Find(Articulo.IdTalle);
                    //Articulo.Talle.TipoTalle = _dbContext.TipoTalle.Find(Articulo.Talle.IdTipoTalle);
                    //Articulo.Color = _dbContext.ColorArticulo.Find(Articulo.IdColor);
                    //Articulo.Categoria = _dbContext.Categoria.Find(Articulo.IdCategoria);
                    LineaDeVenta.Articulo = _dbContext.Articulo.Find(LineaDeVenta.IdArticulo);
                    listaLineaDeVentasDTO.Add(_mapper.Map<LineaDeVenta>(LineaDeVenta));
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaLineaDeVentasDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpGet]
        [Route("{idLineaDeVenta}")]
        public async Task<ActionResult> ObtenerLineaDeVenta(int idLineaDeVenta)
        {
            var responseApi = new ResponseAPI<LineaDeVenta>();
            var LineaDeVentaDTO = new LineaDeVenta();

            try
            {
                var dbLineaDeVenta = await _dbContext.LineaDeVenta.FirstOrDefaultAsync(f => f.IdLineaDeVenta == idLineaDeVenta);


                if (dbLineaDeVenta != null)
                {

                    LineaDeVentaDTO = _mapper.Map<LineaDeVenta>(dbLineaDeVenta);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = LineaDeVentaDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "LineaDeVenta no encontrado";
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
        public async Task<ActionResult> AgregarLineaDeVenta(LineaDeVenta LineaDeVentaDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbLineaDeVenta = _mapper.Map<LineaDeVentaDTO>(LineaDeVentaDTO);

                dbLineaDeVenta.Articulo = _dbContext.Articulo.FirstOrDefault(a => a.IdCodigo == LineaDeVentaDTO.IdArticulo);
                dbLineaDeVenta.Venta = _dbContext.Venta.FirstOrDefault(v => v.IdVenta == LineaDeVentaDTO.IdVenta);

                _dbContext.LineaDeVenta.Add(dbLineaDeVenta);
                await _dbContext.SaveChangesAsync();

                if (dbLineaDeVenta.IdLineaDeVenta != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbLineaDeVenta.IdLineaDeVenta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al LineaDeVenta";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut("{IdLineaDeVenta}")]
        public async Task<ActionResult> ModificarLineaDeVenta(int IdLineaDeVenta, LineaDeVenta LineaDeVentaDTO)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbLineaDeVenta = await _dbContext.LineaDeVenta.Where(c => c.IdLineaDeVenta == IdLineaDeVenta).FirstOrDefaultAsync();

                if (dbLineaDeVenta == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "LineaDeVenta no encontrado";
                    return NotFound(responseApi);
                }

                // Update properties of dbLineaDeVenta with values from LineaDeVentaDTO
                _mapper.Map(LineaDeVentaDTO, dbLineaDeVenta);


                _dbContext.Entry(dbLineaDeVenta).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbLineaDeVenta.IdLineaDeVenta;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejar la excepción específica de concurrencia aquí
                // Puedes agregar el código necesario para manejar esta situación
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Error de concurrencia al intentar modificar el LineaDeVenta. No se suministraron las claves primarias correctamente.";
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
        [Route("{IdLineaDeVenta}")]
        public async Task<IActionResult> EliminarLineaDeVenta(int IdLineaDeVenta)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbLineaDeVenta = await _dbContext.LineaDeVenta.FirstOrDefaultAsync(f => f.IdLineaDeVenta == IdLineaDeVenta);
                if (dbLineaDeVenta != null)
                {
                    _dbContext.LineaDeVenta.Remove(dbLineaDeVenta);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "LineaDeVenta no encontrado";
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
            var responseApi = new ResponseAPI<LineaDeVenta>();
            var LineaDeVentaDTO = new LineaDeVenta();

            try
            {
                var ultimaLineaDeVenta = await _dbContext.LineaDeVenta
            .OrderByDescending(l => l.IdLineaDeVenta)
            .FirstOrDefaultAsync();


                if (ultimaLineaDeVenta != null)
                {

                    LineaDeVentaDTO = _mapper.Map<LineaDeVenta>(ultimaLineaDeVenta);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = LineaDeVentaDTO;
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

