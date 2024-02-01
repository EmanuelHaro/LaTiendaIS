using LaTiendaIS.Shared;

namespace LaTiendaIS.Client.Service.Contrato
{
    public interface IArticuloServicio
    {
        Task<List<ArticuloDTO>> ListarArticulos();
        Task<ArticuloDTO> ObtenerArticulo(int idArticulo);
        Task<int> AgregarArticulo(ArticuloDTO Articulo);
        Task<int> ModificarArticulo(int idArticulo, ArticuloDTO Articulo);
        Task<bool> EliminarArticulo(int idArticulo);
    }
}
