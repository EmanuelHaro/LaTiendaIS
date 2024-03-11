using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IArticuloServicio
    {
        Task<List<Articulo>> ListarArticulos();
        Task<Articulo> ObtenerArticulo(int idArticulo);
        Task<int> AgregarArticulo(Articulo Articulo);
        Task<int> ModificarArticulo(int IdCodigo, Articulo articulo);
        Task<bool> EliminarArticulo(int idArticulo);
    }
}
