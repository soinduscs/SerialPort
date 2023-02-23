using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Soindus.Interfaces.Repositorios.Generico
{
    public interface IRepositorioGenerico<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Dispose();
        void Edit(T entity);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> GetWithFilter(Expression<Func<T, bool>> predicate);
        void Save();
    }
}