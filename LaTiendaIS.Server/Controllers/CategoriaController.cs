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
    public class CategoriaController : ControllerBase
    {
        private DBLaTiendaContext _dbContext;
        private readonly IMapper _mapper;

        public CategoriaController(DBLaTiendaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarCategoria(Categoria Categoria)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbCategoria = _mapper.Map<CategoriaDTO>(Categoria);


                _dbContext.Categoria.Add(dbCategoria);
                await _dbContext.SaveChangesAsync();

                if (dbCategoria.IdCategoria != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbCategoria.IdCategoria;
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
        [Route("{descCategoria}")]
        public async Task<ActionResult> ObtenerCategoria(string descCategoria) //antes pasaba talle y color como parametro
        {
            var responseApi = new ResponseAPI<Categoria>();
            var Categoria = new Categoria();

            try
            {
                var dbCategoria = await _dbContext.Categoria.FirstOrDefaultAsync(f => f.DescripcionCategoria == descCategoria);


                if (dbCategoria != null)
                {

                    Categoria = _mapper.Map<Categoria>(dbCategoria);

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Categoria;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Articulo no encontrado";
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
