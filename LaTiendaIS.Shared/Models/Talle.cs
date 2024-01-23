using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class Talle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //No incremental
        public int IdTalle { get; set; }
        public string DescripcionTalle { get; set; }
    }
}
