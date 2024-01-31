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
    public class PuntoDeVentaDTO
    {
        public int IdPuntoDeVenta { get; set; }

        public int IdSucursal { get; set; }
        public virtual SucursalDTO Sucursal { get; set; }
    }
}
