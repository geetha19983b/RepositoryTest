using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.GenericInterface;
using System.Data.Entity;

namespace DataAccess.GenericAbstract
{
    public class EfRepositoryBase: IRepositoryBase
    {
        protected DbContext Context;

        public EfRepositoryBase(DbContext dbCtx)
        {
            Context = dbCtx;
        }
        public int ExecuteScalar(string query)
        {

            return Context.Database.ExecuteSqlCommand(query);
        }


        public IEnumerable<T> ExecuteReader<T>(string query, object parameters)
        {
           
            return Context.Database.SqlQuery<T>(query, parameters);
        }
    }
}
