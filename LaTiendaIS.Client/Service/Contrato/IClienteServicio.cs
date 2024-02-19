using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IClienteServicio
    {
        Task<ClienteDTO> ObtenerCliente(int idCliente);
        Task<int> AgregarCliente(ClienteDTO Cliente);
        Task<ClienteDTO> ObtenerUltimaCliente();
    }
}
