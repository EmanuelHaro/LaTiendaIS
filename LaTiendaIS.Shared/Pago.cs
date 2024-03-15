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
    public class Pago
    {
        public int IdPago { get; set; }
        public decimal Cantidad { get; set; }

        public int IdVenta { get; set; }
        public virtual Venta? Venta { get; set; }

        public int IdCliente { get; set; }
        public virtual Cliente? Cliente { get; set; }
    }
}
