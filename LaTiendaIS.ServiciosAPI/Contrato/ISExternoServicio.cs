using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface ISExternoServicio
    {
        Task<string> ObtenerToken();
        Task<bool> ConfirmarPago(string token);
    }
}
