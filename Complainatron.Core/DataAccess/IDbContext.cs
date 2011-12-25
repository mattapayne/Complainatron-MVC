using System;

namespace Complainatron.Core.DataAccess
{
    public interface IDbContext : IDisposable
    {
        IDbContextSet<T> Set<T>() where T : class;
        IDbPropertyValues GetCurrentValues<T>(T entry) where T : class;
        int SaveChanges();
    }
}
