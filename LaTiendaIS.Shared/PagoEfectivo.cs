using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class PagoEfectivo: Pago
    {
        public decimal Monto { get; set; }
    }
}
