using AutoMapper;
using LaTiendaIS.ServiciosAPI.Contrato;
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
        private readonly IComprobanteServicio _ComprobanteServicio;

        public ComprobanteController(IComprobanteServicio ComprobanteServicio)
        {
            _ComprobanteServicio = ComprobanteServicio;
        }

        [HttpGet]
        [Route("{IdComprobante}")]
        public async Task<ActionResult> ObtenerComprobante(int IdComprobante)
        {
            var responseApi = new ResponseAPI<Comprobante>();

            try
            {
                var comprobante = await _ComprobanteServicio.ObtenerComprobante(IdComprobante);

                if (comprobante != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = comprobante;
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
        public async Task<ActionResult> AgregarComprobante(Comprobante Comprobante)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var comprobanteAgregado = await _ComprobanteServicio.AgregarComprobante(Comprobante);

                if (comprobanteAgregado)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = comprobanteAgregado;
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
