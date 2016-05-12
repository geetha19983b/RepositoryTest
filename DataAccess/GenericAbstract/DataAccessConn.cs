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
        //private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
       // private static readonly string conString = null;

       // protected static IDbConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SampleDB"].ConnectionString);

        public static IDbConnection GetDbConnection()
        {
            IDbConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SampleDB"].ConnectionString);
            return _connection;
        }

    }

}
