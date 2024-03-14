using LaTiendaIS.ServiciosAPI.Contrato;
using LaTiendaIS.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaTiendaIS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoConTarjetaController : ControllerBase
    {
        private readonly IPagoConTarjetaServicio _pagoServicio;

        public PagoConTarjetaController(IPagoConTarjetaServicio pagoServicio)
        {
            _pagoServicio = pagoServicio;
        }

        [HttpGet]
        [Route("{IdPago}")]
        public async Task<ActionResult> ObtenerPago(int IdPago)
        {
            var responseApi = new ResponseAPI<Pago>();

            try
            {
                var Pago = await _pagoServicio.ObtenerPago(IdPago);

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

        [HttpPost]
        public async Task<ActionResult> AgregarPago(PagoConTarjeta Pago)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                var PagoAgregado = await _pagoServicio.AgregarPago(Pago);

                if (PagoAgregado)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = PagoAgregado;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar al Pago";
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
