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
    public class ComprobanteController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public ComprobanteController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        [HttpGet]
        [Route("{IdComprobante}")]
        public async Task<ActionResult> ObtenerComprobante(int IdComprobante)
        {
            var responseApi = new ResponseAPI<Comprobante>();
            var ComprobanteDTO = new Comprobante();

            try
            {
                var dbComprobante = await _dbContext.Comprobante.FirstOrDefaultAsync(f => f.IdComprobante == IdComprobante);


                if (dbComprobante != null)
                {


                    ComprobanteDTO = _mapper.Map<Comprobante>(dbComprobante);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ComprobanteDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Comprobante no encontrado";
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
        public async Task<ActionResult> AgregarComprobante(Comprobante ComprobanteDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbComprobante = _mapper.Map<ComprobanteDTO>(ComprobanteDTO);


                _dbContext.Comprobante.Add(dbComprobante);
                await _dbContext.SaveChangesAsync();

                if (dbComprobante.IdComprobante != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbComprobante.IdComprobante;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al Comprobante";
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
