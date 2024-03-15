using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeComprobanteController : ControllerBase
    {
        private readonly ITipoDeComprobanteServicio _tipoDeComprobanteServicio;

        public TipoDeComprobanteController(ITipoDeComprobanteServicio tipoDeComprobanteServicio)
        {
            _tipoDeComprobanteServicio = tipoDeComprobanteServicio;
        }


        [HttpGet]
        [Route("Factura/{condtributaria}")]
        public async Task<ActionResult> CalcularFactura(string condtributaria)
        {
            var responseApi = new ResponseAPI<TipoDeComprobante>();

            try
            {
                var comprobante = await _tipoDeComprobanteServicio.ObtenerComprobanteConCondicionTributaria(condtributaria);

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
    }
}
