using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Infosys.FoundationLibrary.DataAccess.GenericInterface;
using Dapper;

namespace Infosys.FoundationLibrary.DataAccess.GenericAbstract
{
    public class DapperDataAccess : DataAccessCon, IGenericDataAccess
    {


        public int ExecuteNonQuery(string query, object parameters, string type = null)
        {
            if (!string.IsNullOrEmpty(type))
            {
                using (var _connection = GetDbConnection())
                {

                    return _connection.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            else
            {
                using (var _connection = GetDbConnection())
                {
                    return _connection.Execute(query, parameters);
                }
            }

        }
        public IEnumerable<TResult> ExecuteReader<TResult>(string query, object parameters, string type = null)
        {
            if (!string.IsNullOrEmpty(type))
            {
                using (var _connection = GetDbConnection())
                {
                    return _connection.Query<TResult>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            else
            {
                using (var _connection = GetDbConnection())
                {
                    return _connection.Query<TResult>(query, parameters);
                }
            }
        }

        public TResult ExecuteScalar<TResult>(string query, object parameters)
        {
            using (var _connection = GetDbConnection())
            {
                return _connection.ExecuteScalar<TResult>(query, parameters);
            }
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> FindBy<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Edit<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }



    }
}
