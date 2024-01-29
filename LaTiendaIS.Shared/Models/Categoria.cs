using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string DescripcionCategoria{ get; set; }


    }
}
