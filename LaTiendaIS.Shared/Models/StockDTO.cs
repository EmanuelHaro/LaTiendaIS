using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class StockDTO
    {
        [Key]
        public int IdStock { get; set; }

        public int Cantidad { get; set; }


        public int IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public virtual SucursalDTO Sucursal { get; set; }

        public int IdArticulo { get; set; } 
        [ForeignKey("IdArticulo")] 
        public virtual ArticuloDTO Articulo { get; set; }

        public int IdTalle { get; set; } //ID FK Talle
        [ForeignKey("IdTalle")] //FK Talle
        public virtual TalleDTO? Talle { get; set; }

        public int IdColor { get; set; } //ID FK Color
        [ForeignKey("IdColor")] //FK Color
        public virtual ColorArticuloDTO? Color { get; set; }
    }
}
