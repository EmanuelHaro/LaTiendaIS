using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IVentaServicio
    {
        Task<List<Venta>> ListarVentas();
        Task<Venta> ObtenerVenta(int idVenta);
        Task<Venta> ObtenerUltimaVenta();
        Task<bool> AgregarVenta(Venta Venta);
        Task<bool> ModificarVenta(int idVenta, Venta Venta);
        Task<bool> EliminarVenta(int idVenta);
    }
}
