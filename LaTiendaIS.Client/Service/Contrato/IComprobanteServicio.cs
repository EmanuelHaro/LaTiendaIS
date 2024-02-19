using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IComprobanteServicio
    {
        Task<ComprobanteDTO> ObtenerComprobante(int idComprobante);
        Task<int> AgregarComprobante(ComprobanteDTO Comprobante);
    }
}
