using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Complainatron.Core.DataAccess;

namespace Complainatron.DataAccess.EntityFramework
{
    public class EntityFrameworkDbContextSet<T> : IDbContextSet<T> where T : class
    {
        private readonly DbSet<T> _adaptedSet;
        private readonly DbContext _context;

        public EntityFrameworkDbContextSet(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
            _adaptedSet = _context.Set<T>();
        }

        public T Create()
        {
            return _adaptedSet.Create();
        }

        public void Add(T item)
        {
            _adaptedSet.Add(item);
        }

        public void Remove(T item)
        {
            _adaptedSet.Remove(item);
        }

        public void Update(T item)
        {
            _context.Entry(item).State = System.Data.EntityState.Modified;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _adaptedSet.Where(predicate);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IQueryable<T>)_adaptedSet).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IQueryable<T>)_adaptedSet).GetEnumerator();
        }

        public Type ElementType
        {
            get { return ((IQueryable<T>)_adaptedSet).ElementType; }
        }

        public Expression Expression
        {
            get { return ((IQueryable<T>)_adaptedSet).Expression; }
        }

        public IQueryProvider Provider
        {
            get { return ((IQueryable<T>)_adaptedSet).Provider; }
        }
    }
}
