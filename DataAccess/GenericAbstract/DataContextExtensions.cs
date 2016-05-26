using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Infosys.FoundationLibrary.DataAccess.GenericInterface;

namespace Infosys.FoundationLibrary.DataAccess.GenericAbstract
{
    public static class DataContextExtensions
    {
        //public static DbContext BulkInsert<T>(this DbContext context, T entity, int count, int batchSize) where T : class
        //{
        //    context.Set<T>().Add(entity);

        //    if (count % batchSize == 0)
        //    {
        //        context.SaveChanges();
        //        context.Dispose();
                
        //        // This is optional
        //        context.Configuration.AutoDetectChangesEnabled = false;
        //    }
        //    return context;
        //}
     
    }
}
