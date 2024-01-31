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
    public class PagoDTO
    {
        public int IdPago { get; set; }
        public double Cantidad { get; set; }

        public int IdVenta { get; set; }
        public virtual VentaDTO Venta { get; set; }
    }
}
