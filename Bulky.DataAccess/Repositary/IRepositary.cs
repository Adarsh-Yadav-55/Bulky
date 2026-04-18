using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public interface IRepositary<T> where T :class
    {
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked=false);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
