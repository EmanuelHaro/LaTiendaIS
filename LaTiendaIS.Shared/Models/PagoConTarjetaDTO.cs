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

        public string NombreTitular { get; set; }

    }
}
