using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class Stock
    {
        public Stock(int cantidad, ColorArticulo color, Talle talle, Articulo articulo)
        {
            Cantidad = cantidad;
            Color = color;
            Talle = talle;
            Articulo = articulo;
        }

        public Stock()
        {
            
        }

        public int IdStock { get; set; }

        public int Cantidad { get; set; }
        public int IdSucursal { get; set; }
        public virtual Sucursal? Sucursal { get; set; }


        public int IdArticulo { get; set; }
        public virtual Articulo? Articulo { get; set; }

        public int IdTalle { get; set; } 
        public virtual Talle? Talle { get; set; }

        public int IdColor { get; set; } 
        public virtual ColorArticulo? Color { get; set; }

    }
}
