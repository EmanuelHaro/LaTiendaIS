using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface ICondicionTServicio
    {

        Task<CondicionTributaria> ObtenerCondicionTributaria(string descCondicion);

        
    }
}
