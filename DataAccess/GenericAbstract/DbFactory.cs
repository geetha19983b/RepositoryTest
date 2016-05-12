using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Infosys.FoundationLibrary.DataAccess.GenericInterface;
namespace Infosys.FoundationLibrary.DataAccess.GenericAbstract
{
    public class DbFactory<TContext> : Disposable, IDbFactory<TContext> where TContext : DbContext, new()
    {
        private TContext dbContext;

        public TContext Init()
        {
            return dbContext ?? (dbContext = new TContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
