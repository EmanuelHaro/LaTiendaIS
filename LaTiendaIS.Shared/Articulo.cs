using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class Articulo
    {
        public int IdCodigo { get; set; }
        public int CodigoTienda { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public float MargenDeGanacia { get; set; }
        public float PorcentajeIVA { get; set; }

        // Campos calculados
        public float NetoGravado
        {
            get { return (float)(Costo + Costo * (MargenDeGanacia / 100)); }
        }

        public float IVA
        {
            get { return NetoGravado * (PorcentajeIVA); }
        }

        public double PrecioDeVenta
        {
            get { return NetoGravado + IVA; }
        }


        public int IdMarca { get; set; } //ID FK Marca
        public virtual MarcaDTO? Marca { get; set; }

        public int IdCategoria { get; set; } //ID FK Categoria
        public virtual CategoriaDTO? Categoria { get; set; }

       
    }
}
