using LaTiendaIS.Shared;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IArticuloServicio
    {
        Task<List<Articulo>> ListarArticulos();
        Task<Articulo> ObtenerArticulo(int idArticulo);
        Task<bool> AgregarArticulo(Articulo Articulo);
        Task<bool> ModificarArticulo(int IdCodigo, Articulo articulo);
        Task<bool> EliminarArticulo(int idArticulo);
    }
}
