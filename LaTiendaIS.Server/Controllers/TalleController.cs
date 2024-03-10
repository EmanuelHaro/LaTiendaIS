using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LaTiendaIS.Shared;
using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalleController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public TalleController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarTalle(Talle Talle)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbTalle = _mapper.Map<TalleDTO>(Talle);


                _dbContext.Talle.Add(dbTalle);
                await _dbContext.SaveChangesAsync();

                if (dbTalle.IdTalle != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTalle.IdTalle;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al Articulo";
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
        [Route("{descTalle}")]
        public async Task<ActionResult> ObtenerTalle(string descTalle) 
        {
            var responseApi = new ResponseAPI<Talle>();
            var Talle = new Talle();

            try
            {
                var dbTalle = await _dbContext.Talle.FirstOrDefaultAsync(f => f.DescripcionTalle == descTalle);


                if (dbTalle != null)
                {
                    Talle = _mapper.Map<Talle>(dbTalle);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Talle;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Talle no encontrado";
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
