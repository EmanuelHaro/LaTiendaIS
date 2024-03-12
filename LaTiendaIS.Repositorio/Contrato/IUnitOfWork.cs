using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Repositorio.Contrato
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericoRepositorio<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
