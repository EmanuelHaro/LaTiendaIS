using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class Marca
    {
        [Key]
        public int IdMarca { get; set; }
        public string DescripcionMarca { get; set; }
    }
}
