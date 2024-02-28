using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IComprobanteServicio
    {
        Task<Comprobante> ObtenerComprobante(int idComprobante);
        Task<int> AgregarComprobante(Comprobante Comprobante);
    }
}
