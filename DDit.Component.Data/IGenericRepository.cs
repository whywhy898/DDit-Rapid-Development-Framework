using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Component.Data
{
    interface IGenericRepository<TEntity> 
        : IDisposable where TEntity : class
    {
        IQueryable<TEntity> Get(
            Expression<Func<TEntity,bool>> filter=null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,
            string includeProperties="");

        IQueryable<TEntity> ExecuteSQLQuery<TEntity>(string sql,SqlParameter[] parm=null);

        TEntity GetByID(object id);

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entityL);
        void Delete(object id);
        void Delete(List<TEntity> idl);
        void Update(TEntity entity);
        void UpdateSup(TEntity entity,List<string> list,bool flag);

        void Save();
    }
}
