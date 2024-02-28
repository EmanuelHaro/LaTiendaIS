using Microsoft.EntityFrameworkCore;

namespace LaTiendaIS.Shared.Models
{
    public class DBLaTiendaContext : DbContext
    {
        public DBLaTiendaContext(DbContextOptions<DBLaTiendaContext> options) : base(options)
        {

        }

        public DbSet<ArticuloDTO> Articulo { get; set; }
        public DbSet<CategoriaDTO> Categoria { get; set; }
        public DbSet<ClienteDTO> Cliente { get; set; }
        public DbSet<ColorArticuloDTO> ColorArticulo { get; set; }
        public DbSet<ComprobanteDTO> Comprobante { get; set; }
        public DbSet<CondicionTributariaDTO> CondicionTributaria { get; set; }
        public DbSet<LineaDeVentaDTO> LineaDeVenta { get; set; }
        public DbSet<MarcaDTO> Marca{ get; set; }
        public DbSet<PagoDTO> Pago { get; set; }
        public DbSet<PagoConTarjetaDTO> PagoConTarjeta { get; set; }
        public DbSet<PagoEfectivoDTO> PagoEfectivo { get; set; }
        public DbSet<PuntoDeVentaDTO> PuntoDeVenta { get; set; }
        public DbSet<StockDTO> Stock { get; set; }
        public DbSet<SucursalDTO> Sucursal { get; set; }
        public DbSet<TalleDTO> Talle { get; set; }
        public DbSet<TipoDeComprobanteDTO> TipoDeComprobante { get; set; }
        public DbSet<TipoTalleDTO> TipoTalle { get; set; }
        public DbSet<VentaDTO> Venta { get; set; }
        




    }
}
