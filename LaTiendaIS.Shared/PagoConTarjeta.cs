using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class PagoConTarjeta: Pago
    {
        public long NumeroDeTarjeta { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CVV { get; set; }

        public string NombreTitular { get; set; }
    }
}
