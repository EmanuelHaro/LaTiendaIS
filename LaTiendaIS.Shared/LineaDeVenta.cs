using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class LineaDeVenta
    {
        public int IdLineaDeVenta { get; set; }

        public int Cantidad { get; set; }

        // Relación de navegación
        public int IdArticulo { get; set; }
        public virtual Articulo? Articulo { get; set; }


        public int IdVenta { get; set; }
        public virtual Venta? Venta { get; set; }
    }
}
