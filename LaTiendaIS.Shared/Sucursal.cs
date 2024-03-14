using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class Sucursal
    {
        public int IdSucursal { get; set; }

        public string? Nombre { get; set; }

        public int IdTienda { get; set; }
        [ForeignKey("IdTienda")]
        public virtual TiendaDTO? Tienda { get; set; }
    }
}
