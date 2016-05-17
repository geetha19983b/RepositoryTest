using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Infosys.FoundationLibrary.DataAccess.GenericAbstract;
namespace Infosys.FoundationLibrary.DataAccess.GenericInterface
{
   
    public interface IGenericDataAccess
    {
        int ExecuteNonQuery(string query, object parameters, string type = null);
        int ExecuteNonQuery<T>(string query, List<T> parameters, out List<object> returnobj, string type = null);
        IEnumerable<TResult> ExecuteReader<TResult>(string query, object parameters, string type = null);
        IEnumerable<TResult> ExecuteReader<TResult, T>(string query, List<T> parameters, out List<object> returnobj, string type = null) where T : class;
        IEnumerable<object> ExecuteReaderMul<T>(string query, List<T> parameters, out List<object> returnobj, string type = null) where T : class;
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
