using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Demo.Framework.Abstract
{
    public interface IDatabaseContext
    {
        int SaveChanges();

        DbEntityEntry Entry(object entity);

        void Dispose();

        DbChangeTracker ChangeTracker { get; }

        DbSet Set(Type entityType);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Database Database { get; }
    }
}
