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
    public class MarcaController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public MarcaController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarMarca(Marca marca)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbMarca = _mapper.Map<MarcaDTO>(marca);


                _dbContext.Marca.Add(dbMarca);
                await _dbContext.SaveChangesAsync();

                if (dbMarca.IdMarca != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbMarca.IdMarca;
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
        [Route("{descMarca}")]
        public async Task<ActionResult> ObtenerMarca(string descMarca) 
        {
            var responseApi = new ResponseAPI<Marca>();
            var marca = new Marca();

            try
            {
                var dbMarca = await _dbContext.Marca.FirstOrDefaultAsync(f => f.DescripcionMarca == descMarca);


                if (dbMarca != null)
                {
                    marca = _mapper.Map<Marca>(dbMarca);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = marca;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Marca no encontrado";
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
