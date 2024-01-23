using Microsoft.EntityFrameworkCore;

namespace LaTiendaIS.Shared.Models
{
    public class DBLaTiendaContext : DbContext
    {
        public DBLaTiendaContext(DbContextOptions<DBLaTiendaContext> options) : base(options)
        {

        }

        //public DbSet<Empleado> Empleado { get; set; }

    }
}
