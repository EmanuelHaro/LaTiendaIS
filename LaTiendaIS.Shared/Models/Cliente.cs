using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string CUIT { get; set; }
        public string Nombre { get; set; }
        public string? Domicilio { get; set; }


        public int IdCondicionTributaria { get; set; }
        [ForeignKey("IdCondicionTributaria")]
        public virtual CondicionTributaria? CondicionTributaria { get; set; }
    }
}
