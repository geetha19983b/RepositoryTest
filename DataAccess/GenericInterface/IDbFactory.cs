using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Infosys.FoundationLibrary.DataAccess.GenericInterface
{
    public interface IDbFactory<TContext> : IDisposable  where TContext : DbContext,new()
    {
        TContext Init();
    }
}
