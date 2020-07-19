using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using Demo.Framework.Abstract;

namespace Demo.Framework.Concrete
{
    public sealed class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public DbSet<T> DbSet { get; set; }

        public BaseRepository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            DbSet = _unitOfWork.GetDatabaseContext().Set<T>();
        }

        /// <summary>
        /// Returns the object with the primary key specifies or throws
        /// </summary>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T Single(object primaryKey)
        {
            var dbResult = DbSet.Find(primaryKey);
            return dbResult;
        }

        public IEnumerable ExecWithStoreProcedure(Type type, string query, params object[] parameters)
        {
            return _unitOfWork.GetDatabaseContext().Database.SqlQuery(type, query, parameters);
        }

        public int ExecuteSqlCommand(string query, params object[] parameters)
        {
            return _unitOfWork.GetDatabaseContext().Database.ExecuteSqlCommand(query, parameters);
        }

        /// <summary>
        /// Returns the object with the primary key specifies or the default for the type
        /// </summary>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T SingleOrDefault(object primaryKey)
        {
            var dbResult = DbSet.Find(primaryKey);
            return dbResult;
        }

        //public virtual IEnumerable<T> GetAllWithDeleted()
        //{
        //    var dbresult = _unitOfWork.Db.Fetch<T>("");

        //    return dbresult;
        //}

        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        //public virtual int Insert(T entity)
        //{
        //    dynamic obj = dbSet.Add(entity);
        //    this._unitOfWork.Db.SaveChanges();
        //    return obj.ID;

        //}
        public T Insert(T entity)
        {
            dynamic obj = DbSet.Add(entity);
            _unitOfWork.GetDatabaseContext().SaveChanges();
            return obj;
        }

        public Guid InsertGuid(T entity)
        {
            dynamic obj = DbSet.Add(entity);
            _unitOfWork.GetDatabaseContext().SaveChanges();
            return obj.ID;

        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            _unitOfWork.GetDatabaseContext().Entry(entity).State = EntityState.Modified;
            _unitOfWork.GetDatabaseContext().SaveChanges();
        }

        //public virtual int Delete(T entity)
        //{
        //    if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
        //    {
        //        dbSet.Attach(entity);
        //    }
        //    dynamic obj = dbSet.Remove(entity);
        //    this._unitOfWork.Db.SaveChanges();
        //    return obj.ID;
        //}

        public void Delete(T entity)
        {
            if (_unitOfWork.GetDatabaseContext().Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
            _unitOfWork.GetDatabaseContext().SaveChanges();
            // return obj.ID;
        }

        public IQueryableUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        internal IDatabaseContext Database
        {
            get
            {
                return _unitOfWork.GetDatabaseContext();
            }
        }

        public Dictionary<string, string> GetAuditNames(dynamic dynamicObject)
        {
            throw new NotImplementedException();
        }

        public List<Dictionary<string, object>> DynamicRead(DbDataReader reader)
        {
            var expandolist = new List<Dictionary<string, object>>();
            foreach (var item in reader)
            {
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(item))
                {
                    var obj = propertyDescriptor.GetValue(item);
                    expando.Add(propertyDescriptor.Name, obj);
                }
                expandolist.Add(new Dictionary<string, object>(expando));
            }
            return expandolist;
        }

        public List<Dictionary<string, object>> GetDynamicResult(string query, params object[] parameters)
        {
            List<Dictionary<string, object>> result;
            using (var cmd = Database.Database.Connection.CreateCommand())
            {
                Database.Database.Connection.Open();
                cmd.CommandText = query;
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                using (var reader = cmd.ExecuteReader())
                {
                    result = DynamicRead(reader).ToList();
                }
            }
            return result;
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public int Count(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            var filters = filter == null ? null : new List<Expression<Func<T, bool>>> { filter };
            var foo = BuildQuery(filters, orderBy, includeProperties).Count();
            return foo;
        }

        public int CountWithFilters(List<Expression<Func<T, bool>>> filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            var foo = BuildQuery(filters, orderBy, includeProperties).Count();
            return foo;
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int? page = null, int pageSize = 10)
        {
            var filters = filter == null ? null : new List<Expression<Func<T, bool>>> { filter };
            return page != null ? BuildQuery(filters, orderBy, includeProperties).Skip((page.Value - 1) * pageSize).Take(pageSize) : BuildQuery(filters, orderBy, includeProperties);
        }

        public IEnumerable<T> GetWithFilters(List<Expression<Func<T, bool>>>
             filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int? page = null, int pageSize = 10)
        {
            return page != null ? BuildQuery(filters, orderBy, includeProperties).Skip((page.Value - 1) * pageSize).Take(pageSize) : BuildQuery(filters, orderBy, includeProperties);
        }

        private IQueryable<T> BuildQuery(List<Expression<Func<T, bool>>> filters, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filters != null)
            {
                query = filters.Aggregate(query, (current, filter) => current.Where(filter));
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
    }
}