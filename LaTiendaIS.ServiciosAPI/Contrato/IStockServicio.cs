using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IStockServicio
    {
        Task<List<Stock>> ObtenerListaDeStockPorArticulo(int idArticulo);
        Task<int> ObtenerCantidad(int codigoTienda, string talle, string color);
        Task<Stock> ObtenerStockPorArticulo(int codigoTienda, string talle, string color);
        Task<bool> ModificarCantidad(int codigoTienda, string talle, string color, Stock stock);
        Task<bool> AgregarCantidadStock(int codigoTienda, string talle, string color, Stock stock);

        Task<Stock> ObtenerStockPorId(int idStock);
    } 
}
