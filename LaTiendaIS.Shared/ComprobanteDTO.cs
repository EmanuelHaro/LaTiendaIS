using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class ComprobanteDTO
    {
        public int IdComprobante { get; set; }

        public int IdVenta { get; set; }
        public virtual VentaDTO? Venta { get; set; }

        public int IdTipoDeComprobante { get; set; }
        public virtual TipoDeComprobanteDTO? TipoDeComprobante { get; set; }
    }
}
