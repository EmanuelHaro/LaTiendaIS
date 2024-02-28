using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class PagoConTarjetaDTO: PagoDTO
    {
        
        public long NumeroDeTarjeta{ get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CVV { get; set; }

        public string NombreTitular { get; set; }

    }
}
