using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;


namespace Infosys.FoundationLibrary.DataAccess.GenericAbstract
{
    public static class DbTypeConverter
    {
        public static DbType TypeToDbType(Type t)
        {
            //Type t = typeof(System.Int32);
       
            DbType dbt;
            try
            {
                dbt = (DbType)Enum.Parse(typeof(DbType), t.Name);
            }
            catch
            {
                dbt = DbType.Object;
            }
            return dbt;
        }
    }
}
