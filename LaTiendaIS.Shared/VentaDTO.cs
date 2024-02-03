using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public double Total { get; set; }


        //// Relación de navegación
        //public virtual ICollection<LineaDeVentaDTO> LineaDeVenta { get; set; }
    }
}
