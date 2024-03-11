using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface ILineaDeVentaServicio
    {
        Task<List<LineaDeVenta>> ListarLineaDeVentas();
        Task<LineaDeVenta> ObtenerLineaDeVenta(int idLineaDeVenta);
        Task<LineaDeVenta> ObtenerUltimaLineaDeVenta();
        Task<int> AgregarLineaDeVenta(LineaDeVenta LineaDeVenta);
        Task<int> ModificarLineaDeVenta(int idLineaDeVenta, LineaDeVenta LineaDeVenta);
        Task<bool> EliminarLineaDeVenta(int idLineaDeVenta);
    }
}