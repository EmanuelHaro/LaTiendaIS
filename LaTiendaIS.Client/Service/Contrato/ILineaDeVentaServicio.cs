using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface ILineaDeVentaServicio
    {
        Task<List<LineaDeVenta>> ListarLineaDeVentas();
        Task<LineaDeVenta> ObtenerLineaDeVenta(int idLineaDeVenta);
        Task<LineaDeVenta> ObtenerUltimaLineaDeVenta();

        Task<LineaDeVenta> ObtenerLineaDeVentaPorArticulo(int idArticulo); 

        Task<bool> AgregarLineaDeVenta(LineaDeVenta LineaDeVenta);
        Task<bool> ModificarLineaDeVenta(int idLineaDeVenta, LineaDeVenta LineaDeVenta);
        Task<bool> EliminarLineaDeVenta(int idLineaDeVenta);
    }
}