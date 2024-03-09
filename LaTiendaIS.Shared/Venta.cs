using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        [Column(TypeName = "decimal(18,2)")] // Optional: Add this attribute if your database column requires specific precision
        public decimal Total { get; set; }


        //// Relación de navegación
        //public virtual ICollection<LineaDeVentaDTO> LineaDeVenta { get; set; }
    }
}
