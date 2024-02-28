using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IArticuloServicio
    {
        Task<List<Articulo>> ListarArticulos();
        Task<Articulo> ObtenerArticulo(int idArticulo, int idTalle, int idColor);
        Task<int> AgregarArticulo(Articulo Articulo);
        Task<int> ModificarArticulo(int idArticulo, Articulo Articulo);
        Task<bool> EliminarArticulo(int idArticulo);
    }
}
