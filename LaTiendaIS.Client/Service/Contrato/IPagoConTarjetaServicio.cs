using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IPagoConTarjetaServicio
    {
        Task<Pago> ObtenerPago(int idPago);
        Task<bool> AgregarPago(PagoConTarjeta Pago);
    }
}
