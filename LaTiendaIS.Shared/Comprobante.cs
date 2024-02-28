using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class Comprobante
    {
        public int IdComprobante { get; set; }

        public int IdVenta { get; set; }
        public virtual Venta? Venta { get; set; }

        public int IdTipoDeComprobante { get; set; }
        public virtual TipoDeComprobante? TipoDeComprobante { get; set; }
    }
}
