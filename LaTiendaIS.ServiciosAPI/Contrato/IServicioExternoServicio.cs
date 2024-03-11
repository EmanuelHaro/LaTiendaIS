using LaTiendaIS.Shared;
using System.Net.Http;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IServicioExternoServicio
    {
        Task<string> ObtenerToken();
        Task<bool> ConfirmarPago(string token);
    }
}
