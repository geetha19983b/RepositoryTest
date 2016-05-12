using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infosys.FoundationLibrary.DataAccess.GenericAbstract;
using System.Data.Entity;

namespace Infosys.FoundationLibrary.DataAccess.GenericInterface
{
    public interface IUnitOfWork<TContext>  where TContext : DbContext,new()
    {
        void Commit();
        EfDataAccess<TContext> EfRepDA { get; }
    }
    
}
