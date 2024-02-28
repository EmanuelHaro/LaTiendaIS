using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class ComprobanteDTO
    {
        [Key]
        public int IdComprobante { get; set; }




        public int IdVenta { get; set; }
        [ForeignKey("IdVenta")]
        public virtual VentaDTO Venta { get; set; }

        public int IdTipoDeComprobante { get; set; }
        [ForeignKey("IdTipoDeComprobante")]
        public virtual TipoDeComprobanteDTO TipoDeComprobante { get; set; }
    }
}
