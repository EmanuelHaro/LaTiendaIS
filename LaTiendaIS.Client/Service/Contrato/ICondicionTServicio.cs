using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface ICondicionTServicio
    {

        Task<CondicionTributaria> ObtenerCondicionTributaria(string descCondicion);
    }
}
