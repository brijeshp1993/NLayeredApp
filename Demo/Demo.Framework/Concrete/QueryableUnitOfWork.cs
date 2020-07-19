using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Demo.Framework.Abstract;
using System.Data.Entity.Core.Objects;
using System.Data.Common;

namespace Demo.Framework.Concrete
{
    /// <summary>
    /// Querable unit of work
    /// </summary>
    public class QueryableUnitOfWork : IQueryableUnitOfWork
    {
        private readonly IDatabaseContext _databaseContext;
        private ObjectContext _objectContext;
        private DbTransaction _transaction;
        private bool _disposed;

        public QueryableUnitOfWork(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #region IQueryableUnitOfWork Members

        /// <summary>
        /// get entity set
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return _databaseContext.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            _databaseContext.Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item)
            where TEntity : class
        {
            //this operation also attach item in object state manager
            _databaseContext.Entry(item).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if it is not attached, attach original and set current values
            _databaseContext.Entry(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            _databaseContext.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    _databaseContext.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter)_databaseContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }

            _transaction = _objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            _databaseContext.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return _databaseContext.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return _databaseContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _databaseContext != null)
            {
                _databaseContext.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        /// get data base context
        /// </summary>
        /// <returns></returns>
        public IDatabaseContext GetDatabaseContext()
        {
            return _databaseContext;
        }

        #endregion
    }
}
