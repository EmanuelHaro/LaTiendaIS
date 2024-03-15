using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LaTiendaIS.ServiciosAPI.Contrato;
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
        private readonly ICategoriaServicio _CategoriaServicio;

        public CategoriaController(ICategoriaServicio CategoriaServicio)
        {
            _CategoriaServicio = CategoriaServicio;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarCategoria(Categoria Categoria)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var categoriaAgregada = await _CategoriaServicio.AgregarCategoria(Categoria);

                if (categoriaAgregada)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = categoriaAgregada;
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

            try
            {
                var categoria = await _CategoriaServicio.ObtenerCategoria(descCategoria);

                if (categoria != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = categoria;
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
