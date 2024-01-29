﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class Articulo
    {
        [Key]
        public int IdCodigo { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public float MargenDeGanacia { get; set; }
        public float PorcentajeIVA { get; set; }

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