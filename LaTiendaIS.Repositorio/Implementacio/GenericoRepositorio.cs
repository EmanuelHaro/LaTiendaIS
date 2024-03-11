using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIS.Repositorio.Implementacio
{
    public class GenericoRepositorio<TModelo> : IGenericoRepositorio<TModelo> where TModelo : class
    {
        private readonly DBLaTiendaContext2 _dbContext;
        public GenericoRepositorio(DBLaTiendaContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TModelo> Obtener(Expression<Func<TModelo, bool>>? filtro = null)
        {
            //Si filtro es nulo no aplico el filtro
            IQueryable<TModelo> consulta = (filtro == null) ? _dbContext.Set<TModelo>() : _dbContext.Set<TModelo>().Where(filtro);
            return consulta;
        }

        public async Task<bool> Crear(TModelo modelo)
        {
            try
            {
                _dbContext.Set<TModelo>().Add(modelo); //_dbcontext.Articulo.Add(articulo) si recibo Set<Articulo>()
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Modificar(TModelo modelo)
        {
            try
            {
                _dbContext.Set<TModelo>().Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {
                _dbContext.Set<TModelo>().Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
