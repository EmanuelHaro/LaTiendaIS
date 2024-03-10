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
    public class CondicionTributariaController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public CondicionTributariaController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{descCondicion}")]
        public async Task<ActionResult> ObtenerCondicionTributaria(string descCondicion)
        {
            var responseApi = new ResponseAPI<CondicionTributaria>();
            var CondicionTributariaDTO = new CondicionTributaria();

            try
            {
                var dbCondicionTributaria = await _dbContext.CondicionTributaria.FirstOrDefaultAsync(f => f.Descripcion == descCondicion);


                if (dbCondicionTributaria != null)
                {
                    CondicionTributariaDTO = _mapper.Map<CondicionTributaria>(dbCondicionTributaria);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = CondicionTributariaDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "CondicionTributaria no encontrado";
                }
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi.Valor);
        }
    }
}