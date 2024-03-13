using LaTiendaIS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.ServiciosAPI.Contrato
{
    public interface ICategoriaServicio
    {
        Task<Categoria> ObtenerCategoria(string descCategoria);
        Task<bool> AgregarCategoria(Categoria Categoria);
    }
}
