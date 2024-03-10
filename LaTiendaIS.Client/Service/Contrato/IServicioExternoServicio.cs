using LaTiendaIS.Shared;
using System.Net.Http;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IServicioExternoServicio
    {
        Task<string> ObtenerToken();
        Task<bool> ConfirmarPago(string token);
    }
}
