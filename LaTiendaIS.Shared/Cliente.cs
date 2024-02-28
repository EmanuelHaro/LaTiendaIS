using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string CUIT { get; set; }
        public string Nombre { get; set; }
        public string? Domicilio { get; set; }


        public int IdCondicionTributaria { get; set; }
        public virtual CondicionTributaria? CondicionTributaria { get; set; }
    }
}
