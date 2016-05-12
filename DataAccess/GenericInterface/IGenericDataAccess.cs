using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosys.FoundationLibrary.DataAccess.GenericInterface
{
    public interface IGenericDataAccess
    {
        int ExecuteNonQuery(string query, object parameters, string type = null);
        IEnumerable<TResult> ExecuteReader<TResult>(string query, object parameters, string type = null);
        TResult ExecuteScalar<TResult>(string query, object parameters);


        void Insert<TEntity>(TEntity entity) where TEntity : class;
        void Save();
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        IQueryable<TEntity> FindBy<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void Edit<TEntity>(TEntity entity) where TEntity : class;
    }
}
