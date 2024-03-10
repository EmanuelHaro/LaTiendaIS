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
    public class SucursalController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public SucursalController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarSucursal(Sucursal Sucursal)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbSucursal = _mapper.Map<SucursalDTO>(Sucursal);


                _dbContext.Sucursal.Add(dbSucursal);
                await _dbContext.SaveChangesAsync();

                if (dbSucursal.IdSucursal != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbSucursal.IdSucursal;
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
        [Route("{descSucursal}")]
        public async Task<ActionResult> ObtenerSucursal(string descSucursal) //antes pasaba talle y color como parametro
        {
            var responseApi = new ResponseAPI<Sucursal>();
            var Sucursal = new Sucursal();

            try
            {
                var dbSucursal = await _dbContext.Sucursal.FirstOrDefaultAsync(f => f.Nombre == descSucursal);


                if (dbSucursal != null)
                {
                    Sucursal = _mapper.Map<Sucursal>(dbSucursal);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Sucursal;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Sucursal no encontrado";
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
