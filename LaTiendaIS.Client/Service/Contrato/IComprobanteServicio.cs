using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IComprobanteServicio
    {
        Task<Comprobante> ObtenerComprobante(int idComprobante);
        Task<bool> AgregarComprobante(Comprobante Comprobante);
    }
}
