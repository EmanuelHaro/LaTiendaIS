using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoServicio _pagoServicio;

        public PagoController(IPagoServicio pagoServicio)
        {
            _pagoServicio = pagoServicio;
        }

        [HttpGet]
        [Route("{IdVenta}")]
        public async Task<ActionResult> ObtenerPagoConVenta(int IdVenta)
        {
            var responseApi = new ResponseAPI<Pago>();

            try
            {
                var Pago = await _pagoServicio.ObtenerConVentaPago(IdVenta);

                if (Pago != null)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = Pago;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Pago no encontrado";
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
