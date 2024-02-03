using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class StockDTO
    {
        public int IdStock { get; set; }

        public int Cantidad { get; set; }
        public int IdSucursal { get; set; }
        public virtual SucursalDTO Sucursal { get; set; }


        public int IdArticulo { get; set; }
        public virtual ArticuloDTO Articulo { get; set; }

    }
}
