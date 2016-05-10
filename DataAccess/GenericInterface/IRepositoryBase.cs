using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.GenericInterface
{
  
    public interface IRepositoryBase
    {
        int ExecuteScalar(string query);
        IEnumerable<T> ExecuteReader<T>(string query, object parameters);
    }
}
