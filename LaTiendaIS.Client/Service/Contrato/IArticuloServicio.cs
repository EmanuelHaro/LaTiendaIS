using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IArticuloServicio
    {
        Task<List<Articulo>> ListarArticulos();
        Task<Articulo> ObtenerArticulo(int idArticulo);
        Task<bool> AgregarArticulo(Articulo Articulo);
        Task<bool> ModificarArticulo(int idArticulo, Articulo Articulo);
        Task<bool> EliminarArticulo(int idArticulo);
    }
}
