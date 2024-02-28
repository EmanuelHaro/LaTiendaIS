using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class TiendaDTO
    {
        [Key]
        public int IdTienda { get; set; }
        public string Nombre { get; set; }
        public long Telefono { get; set; }
        public string Direccion { get; set; }
        public string CUIT { get; set; }
    }
}
