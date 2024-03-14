using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IPagoServicio
    {
        Task<Pago> ObtenerConVentaPago(int idVenta);
    }
}
