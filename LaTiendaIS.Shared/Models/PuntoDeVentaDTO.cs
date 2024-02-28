using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class PuntoDeVentaDTO
    {
        [Key]
        public int IdPuntoDeVenta { get; set; }

        public int NumeroPtoVenta { get; set; }

        public int IdSucursal { get; set; } 
        [ForeignKey("IdSucursal")] 
        public virtual SucursalDTO Sucursal { get; set; }
    }
}
