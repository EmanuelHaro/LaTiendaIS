using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface ILineaDeVentaServicio
    {
        Task<List<LineaDeVenta>> ListarLineaDeVentas();
        Task<LineaDeVenta> ObtenerLineaDeVenta(int idLineaDeVenta);
        Task<LineaDeVenta> ObtenerLineaDeVentaPorArticulo(int idArticulo);
        Task<LineaDeVenta> ObtenerUltimaLineaDeVenta();
        Task<bool> AgregarLineaDeVenta(LineaDeVenta LineaDeVenta);
        Task<bool> ModificarLineaDeVenta(int idLineaDeVenta, LineaDeVenta LineaDeVenta);
        Task<bool> EliminarLineaDeVenta(int idLineaDeVenta);
    }
}