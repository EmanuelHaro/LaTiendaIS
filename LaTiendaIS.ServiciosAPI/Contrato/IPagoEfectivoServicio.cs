using LaTiendaIS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IPagoEfectivoServicio
    {
        Task<Pago> ObtenerPago(int idPago);
        Task<bool> AgregarPago(PagoEfectivo Pago);
    }
}
