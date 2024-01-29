using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class TipoDeComprobante
    {
        [Key]
        public int IdTipoDeComprobante { get; set; }
        public string Descripcion { get; set; }
    }
}
