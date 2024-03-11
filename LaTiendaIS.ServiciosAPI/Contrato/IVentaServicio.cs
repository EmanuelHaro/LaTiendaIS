using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IVentaServicio
    {
        Task<List<Venta>> ListarVentas();
        Task<Venta> ObtenerVenta(int idVenta);
        Task<Venta> ObtenerUltimaVenta();
        Task<int> AgregarVenta(Venta Venta);
        Task<int> ModificarVenta(int idVenta, Venta Venta);
        Task<bool> EliminarVenta(int idVenta);
    }
}
