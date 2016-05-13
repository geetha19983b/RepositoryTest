using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace Infosys.FoundationLibrary.DataAccess.GenericAbstract
{
      public abstract class DataAccessCon
    {
      

          public static string ConString
          {
              get;
              set;
          }

        public static IDbConnection GetDbConnection()
        {
      
            IDbConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings[ConString].ConnectionString);
            return _connection;
        }

    }

}
