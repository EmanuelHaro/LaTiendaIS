using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IClienteServicio
    {
        Task<Cliente> ObtenerCliente(int idCliente);
        Task<bool> AgregarCliente(Cliente Cliente);
        Task<Cliente> ObtenerUltimaCliente();
    }
}
