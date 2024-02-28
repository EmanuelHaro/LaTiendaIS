using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class Stock
    {
        public int IdStock { get; set; }

        public int Cantidad { get; set; }
        public int IdSucursal { get; set; }
        public virtual Sucursal Sucursal { get; set; }


        public int IdArticulo { get; set; }
        public virtual Articulo Articulo { get; set; }

        public int IdTalle { get; set; } //ID FK Talle
        public virtual TalleDTO? Talle { get; set; }

        public int IdColor { get; set; } //ID FK Color
        public virtual ColorArticuloDTO? Color { get; set; }

    }
}
