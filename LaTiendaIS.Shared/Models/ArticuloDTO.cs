using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class ArticuloDTO
    {
        [Key]
        public int IdCodigo { get; set; }
        public int CodigoTienda { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public float MargenDeGanacia { get; set; }
        public float PorcentajeIVA { get; set; }

        public int IdMarca { get; set; } //ID FK Marca
        [ForeignKey("IdMarca")] //FK Marca
        public virtual MarcaDTO? Marca { get; set; }

        public int IdCategoria { get; set; } //ID FK Categoria
        [ForeignKey("IdCategoria")] //FK Categoria
        public virtual CategoriaDTO? Categoria { get; set; }




    }
}
