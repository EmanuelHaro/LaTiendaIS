using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class Stock
    {
        [Key]
        public int IdStock { get; set; }

        //public long IdCategoria { get; set; } //ID FK Categoria
        //[ForeignKey("IdCategoria")] //FK Categoria
        //public virtual Categoria? Categoria { get; set; }
    }
}
