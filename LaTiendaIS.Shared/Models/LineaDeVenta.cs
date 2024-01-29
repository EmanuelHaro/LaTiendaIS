using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class LineaDeVenta
    {
        [Key]
        public int IdLineaDeVenta { get; set; }

        public int Cantidad { get; set; }

        // Relación de navegación
        public int IdArticulo { get; set; }
        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }


        public int IdVenta { get; set; }
        [ForeignKey("IdVenta")]
        public virtual Venta Venta { get; set; }

    }
}
