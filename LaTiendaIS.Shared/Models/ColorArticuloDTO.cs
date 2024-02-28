using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class ColorArticuloDTO
    {
        [Key]
        public int IdColor { get; set; }
        public string DescripcionColor { get; set; }
    }
}
