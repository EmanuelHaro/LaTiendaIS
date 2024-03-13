using LaTiendaIS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IColorArticuloServicio
    {
        Task<ColorArticulo> ObtenerColorArticulo(string descColorArticulo);
        Task<bool> AgregarColorArticulo(ColorArticulo ColorArticulo);
    }
}
