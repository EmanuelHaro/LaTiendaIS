using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IComprobanteServicio
    {
        Task<Comprobante> ObtenerComprobante(int idComprobante);
        Task<bool> AgregarComprobante(Comprobante Comprobante);

        
    }
}
