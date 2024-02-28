using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class VentaDTO
    {
        [Key]
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public double Total { get; set; }

        //// Relación de navegación
        //public virtual ICollection<LineaDeVenta> LineaDeVenta { get; set; }




    }
}


