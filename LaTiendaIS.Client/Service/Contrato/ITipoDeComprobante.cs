using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface ITipoDeComprobante
    {
        Task<TipoDeComprobante> ObtenerComprobanteConCondicionTributaria(string descCondicion);
    }
}
