using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public  class Sucursal
    {
        [Key]
        public int IdSucursal { get; set; }
        
        public string? Nombre { get; set; }

        public int IdTienda { get; set; } 
        [ForeignKey("IdTienda")] 
        public virtual Tienda? Tienda { get; set; }
    }
}
