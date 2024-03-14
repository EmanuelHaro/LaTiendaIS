using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IPagoEfectivoServicio
    {
        Task<Pago> ObtenerPago(int idPago);
        Task<bool> AgregarPago(PagoEfectivo Pago);
    }
}
