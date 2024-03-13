using LaTiendaIS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface IMarcaServicio
    {
        Task<Marca> ObtenerMarca(string descMarca);
        Task<bool> AgregarMarca(Marca Marca);
    }
}
