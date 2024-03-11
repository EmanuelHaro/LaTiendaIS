using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IComprobanteServicio
    {
        Task<Comprobante> ObtenerComprobante(int idComprobante);
        Task<int> AgregarComprobante(Comprobante Comprobante);
    }
}
