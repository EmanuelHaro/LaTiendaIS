using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class Stock
    {
        [Key]
        public int IdStock { get; set; }

        public int Cantidad { get; set; }


        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }

        public int IdArticulo { get; set; } 
        [ForeignKey("IdArticulo")] 
        public virtual Articulo Articulo { get; set; }
    }
}
