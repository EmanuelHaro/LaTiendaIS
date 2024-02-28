using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class TalleDTO
    {
        [Key]
        public int IdTalle { get; set; }
        public string DescripcionTalle { get; set; }


        public int IdTipoTalle{ get; set; } //ID FK TipoTalle
        [ForeignKey("IdTipoTalle")] //FK TipoTalle
        public virtual TipoTalleDTO? TipoTalle { get; set; }
    }
}
