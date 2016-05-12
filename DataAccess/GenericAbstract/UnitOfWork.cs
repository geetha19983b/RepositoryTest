using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infosys.FoundationLibrary.DataAccess.GenericInterface;
using System.Data.Entity;

namespace Infosys.FoundationLibrary.DataAccess.GenericAbstract
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        private readonly IDbFactory<TContext> dbFactory;
        private TContext dbContext;

        public UnitOfWork(IDbFactory<TContext> dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        public EfDataAccess<TContext> EfRepDA
        {
            get { return new EfDataAccess<TContext>(dbFactory); }
        }
        public TContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
