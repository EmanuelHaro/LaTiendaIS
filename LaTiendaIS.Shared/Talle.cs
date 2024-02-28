using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class Talle
    {
        public int IdTalle { get; set; }
        public string DescripcionTalle { get; set; }


        public int IdTipoTalle { get; set; } //ID FK TipoTalle
        public virtual TipoTalle? TipoTalle { get; set; }
    }
}
