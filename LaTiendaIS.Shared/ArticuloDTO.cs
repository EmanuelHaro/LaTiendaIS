using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class ArticuloDTO
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public float MargenDeGanacia { get; set; }
        public float NetoGravado { get; set; } //No sabemos que es
        public float IVA { get; set; }
        public double PrecioDeVenta { get; set; }

        public int IdMarca { get; set; } //ID FK Marca
        [ForeignKey("IdMarca")] //FK Marca
        public virtual Marca? Marca { get; set; }

        public int IdTalle { get; set; } //ID FK Talle
        [ForeignKey("IdTalle")] //FK Talle
        public virtual Talle? Talle { get; set; }

        public int IdColor { get; set; } //ID FK Color
        [ForeignKey("IdColor")] //FK Color
        public virtual ColorArticulo? Color { get; set; }

        public int IdCategoria { get; set; } //ID FK Categoria
        [ForeignKey("IdCategoria")] //FK Categoria
        public virtual Categoria? Categoria { get; set; }
    }
}
