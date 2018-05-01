using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;


namespace DDit.Component.Data
{
    public class GenericRepository<TEntity>
        : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public  GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            this.context.Database.Log = new Action<string>(q => Debug.WriteLine(q));
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entityL)
        {
            try
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var entity in entityL)
                {
                    dbSet.Add(entity);
                }
            }
            finally
            {
                context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(List<TEntity> idl)
        {
            try
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var objid in idl)
                {
                    Delete(objid);
                }
            }
            finally {
                context.Configuration.AutoDetectChangesEnabled = true;
            }
            
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
           
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// 高级修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="list">操作的字段名集合</param>
        /// <param name="flag">默认是true要修改的，false不修改的</param>
        public void UpdateSup(TEntity entity, List<string> list, bool flag=true)
        {
            dbSet.Attach(entity);
            if (flag)
            {
                var stateEntry = ((IObjectContextAdapter)context).ObjectContext.
                    ObjectStateManager.GetObjectStateEntry(entity);
                foreach (var item in list)
                {
                    stateEntry.SetModifiedProperty(item);
                }
            }
            else {
                context.Entry(entity).State = EntityState.Modified;
                Type entityClass = entity.GetType();
                var propertys = entityClass.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var item in list)
                {
                    if (propertys.Where(a => a.Name == item).Any())
                    {
                        context.Entry(entity).Property("" + item + "").IsModified = false;
                    }
                }
            }
        }

        public virtual IQueryable<TEntity> ExecuteSQLQuery<TEntity>(string sql, SqlParameter[] parmArray = null)
        {

           IQueryable<TEntity> result=context.Database.SqlQuery<TEntity>(sql, parmArray).AsQueryable();
           return result;

        }


        public virtual void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}