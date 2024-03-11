using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IClienteServicio
    {
        Task<Cliente> ObtenerCliente(int idCliente);
        Task<int> AgregarCliente(Cliente Cliente);
        Task<Cliente> ObtenerUltimaCliente();
    }
}
