using Microsoft.EntityFrameworkCore;

namespace LaTiendaIS.Shared.Models
{
    public class DBLaTiendaContext : DbContext
    {
        public DBLaTiendaContext(DbContextOptions<DBLaTiendaContext> options) : base(options)
        {

        }

        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Marca> Marca{ get; set; }
        public DbSet<Talle> Talle { get; set; }
        public DbSet<ColorArticulo> ColorArticulo { get; set; }

    }
}
