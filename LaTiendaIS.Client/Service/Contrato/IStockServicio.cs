using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IStockServicio
    {
        Task<List<Stock>> ObtenerListaDeTalleYColorDelStock(int idArticulo);
        Task<int> ObtenerCantidad(int codigoTienda, string talle, string color);
        Task<Stock> ObtenerStockPorArticulo(int codigoTienda, string talle, string color);
        Task<bool> ModificarCantidadStock(int codigoTienda, string talle, string color, Stock stock);
        Task<bool> AgregarCantidadStock(int codigoTienda, string talle, string color, Stock stock);

        Task<Stock> ObtenerStockPorId(int idStock);
    } 
}
