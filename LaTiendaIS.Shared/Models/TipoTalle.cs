﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Shared.Models
{
    public class TipoTalle
    {
        [Key]
        public int IdTipoTalle { get; set; }
        public string DescripcionTipoTalle { get; set; }
    }
}
