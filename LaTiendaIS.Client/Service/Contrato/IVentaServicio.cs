using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IVentaServicio
    {
        Task<List<VentaDTO>> ListarVentas();
        Task<VentaDTO> ObtenerVenta(int idVenta);
        Task<int> AgregarVenta(VentaDTO Venta);
        Task<int> ModificarVenta(int idVenta, VentaDTO Venta);
        Task<bool> EliminarVenta(int idVenta);
    }
}
