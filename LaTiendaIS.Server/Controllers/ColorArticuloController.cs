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
    public class ColorArticuloController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public ColorArticuloController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarColorArticulo(ColorArticulo ColorArticulo)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbColorArticulo = _mapper.Map<ColorArticuloDTO>(ColorArticulo);

                _dbContext.ColorArticulo.Add(dbColorArticulo);
                await _dbContext.SaveChangesAsync();

                if (dbColorArticulo.IdColor != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbColorArticulo.IdColor;
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
        [Route("{descColorArticulo}")]
        public async Task<ActionResult> ObtenerColorArticulo(string descColorArticulo) //antes pasaba talle y color como parametro
        {
            var responseApi = new ResponseAPI<ColorArticulo>();
            var ColorArticulo = new ColorArticulo();

            try
            {
                var dbColorArticulo = await _dbContext.ColorArticulo.FirstOrDefaultAsync(f => f.DescripcionColor == descColorArticulo);


                if (dbColorArticulo != null)
                {
                    ColorArticulo = _mapper.Map<ColorArticulo>(dbColorArticulo);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ColorArticulo;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "ColorArticulo no encontrado";
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
