using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IClienteServicio
    {
        Task<Cliente> ObtenerCliente(int idCliente);
        Task<Cliente> ObtenerClienteAnonimo();
        Task<bool> AgregarCliente(Cliente Cliente);
        Task<Cliente> ObtenerUltimaCliente();

        Task<Cliente> ObtenerClientePorCuit(string cuit);
    }
}
