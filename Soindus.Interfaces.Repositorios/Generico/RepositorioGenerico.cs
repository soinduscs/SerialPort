using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Soindus.Interfaces.Repositorios.Generico
{
    public class RepositorioGenerico<T> : IDisposable, IRepositorioGenerico<T> where T : class
    {
        protected internal readonly DbContext _context;
        protected internal readonly DbSet<T> _dbSet;

        public RepositorioGenerico(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return query;
        }

        public IQueryable<T> GetWithFilter(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
