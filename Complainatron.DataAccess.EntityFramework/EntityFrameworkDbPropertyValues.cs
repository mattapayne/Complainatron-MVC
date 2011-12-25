using System;
using System.Data.Entity.Infrastructure;
using Complainatron.Core.DataAccess;

namespace Complainatron.DataAccess.EntityFramework
{
    public class EntityFrameworkDbPropertyValues : IDbPropertyValues
    {
        private readonly DbPropertyValues _wrappedValues;

        public EntityFrameworkDbPropertyValues(DbPropertyValues wrappedValues)
        {
            if (wrappedValues == null)
            {
                throw new ArgumentNullException("wrappedValues");
            }

            _wrappedValues = wrappedValues;
        }

        public TValue GetValue<TValue>(string key)
        {
            return _wrappedValues.GetValue<TValue>(key);
        }
    }
}
