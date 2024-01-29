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
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ColorArticulo> ColorArticulo { get; set; }
        public DbSet<Comprobante> Comprobante { get; set; }
        public DbSet<CondicionTributaria> CondicionTributaria { get; set; }
        public DbSet<LineaDeVenta> LineaDeVenta { get; set; }
        public DbSet<Marca> Marca{ get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<PagoConTarjeta> PagoConTarjeta { get; set; }
        public DbSet<PagoEfectivo> PagoEfectivo { get; set; }
        public DbSet<PuntoDeVenta> PuntoDeVenta { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<Talle> Talle { get; set; }
        public DbSet<TipoDeComprobante> TipoDeComprobante { get; set; }
        public DbSet<TipoTalle> TipoTalle { get; set; }
        public DbSet<Venta> Venta { get; set; }
        




    }
}
