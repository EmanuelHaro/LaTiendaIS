using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface ILineaDeVentaServicio
    {
        Task<List<LineaDeVentaDTO>> ListarLineaDeVentas();
        Task<LineaDeVentaDTO> ObtenerLineaDeVenta(int idLineaDeVenta);
        Task<int> AgregarLineaDeVenta(LineaDeVentaDTO LineaDeVenta);
        Task<int> ModificarLineaDeVenta(int idLineaDeVenta, LineaDeVentaDTO LineaDeVenta);
        Task<bool> EliminarLineaDeVenta(int idLineaDeVenta);
    }
}