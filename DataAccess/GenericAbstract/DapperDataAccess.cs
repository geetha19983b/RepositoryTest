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

        public DapperDataAccess(string conString)
        {
            ConString = conString;
        }
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


        public IEnumerable<TResult> ExecuteReader<TResult, T>(string query, List<T> parameters, out List<object> returnobj, string type = null) where T : class
        {

            returnobj = new List<object>();
            using (var _connection = GetDbConnection())
            {
               
                List<ParamterTemplate> listparms = parameters.OfType<ParamterTemplate>().ToList();
                var parms = new DynamicParameters();
                listparms.ForEach(a =>
                {
                    if (a.ParameterDirection == "Input")
                    {
                        parms.Add(a.ParamterName, a.ParamterValue,null, ParameterDirection.Input);
                    }
                    else
                    {
                        DbType t = DbTypeConverter.TypeToDbType(a.ParameterType);
                        parms.Add(a.ParamterName, null,t, ParameterDirection.Output);
                    }
                    
                });

                var grid = _connection.QueryMultiple(query,parms,commandType: CommandType.StoredProcedure);
                //var result = _connection.Query<TResult>(query, parms, commandType: CommandType.StoredProcedure).ToList();

                var result = grid.Read<TResult>().ToList();
                
                foreach (var x in listparms)
                {
                    if (x.ParameterDirection == "Output")
                    {
                        
                        returnobj.Add(grid.Read<dynamic>().FirstOrDefault());
                    }
                }
                return result;




            }
        }
    }
}
