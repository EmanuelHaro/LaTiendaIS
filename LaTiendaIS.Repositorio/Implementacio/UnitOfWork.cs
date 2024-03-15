using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Repositorio.Implementacio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBLaTiendaContext _dbContext;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(DBLaTiendaContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public IGenericoRepositorio<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)) == false)
            {
                _repositories[typeof(TEntity)] = new GenericoRepositorio<TEntity>(_dbContext);
            }

            return (IGenericoRepositorio<TEntity>)_repositories[typeof(TEntity)];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
