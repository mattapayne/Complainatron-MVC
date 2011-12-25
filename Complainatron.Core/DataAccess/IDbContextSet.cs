using System;
using System.Linq;
using System.Linq.Expressions;

namespace Complainatron.Core.DataAccess
{
    public interface IDbContextSet<T> : IQueryable<T> where T : class
    {
        T Create();
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}
