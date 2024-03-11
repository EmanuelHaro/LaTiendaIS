using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Repositorio.Contrato
{
    public interface IGenericoRepositorio<TModelo> where TModelo : class
    {
        //Consultar: Seria como un select * from Usuario where Rol = 'Administrador'
        IQueryable<TModelo> Obtener(Expression<Func<TModelo, bool>>? filtro = null);

        Task<bool> Crear(TModelo modelo);

        Task<bool> Modificar(TModelo modelo);

        Task<bool> Eliminar(TModelo modelo);
    }
}
