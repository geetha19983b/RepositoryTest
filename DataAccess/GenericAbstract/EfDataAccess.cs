using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infosys.FoundationLibrary.DataAccess.GenericInterface;
using System.Data.Entity;


namespace Infosys.FoundationLibrary.DataAccess.GenericAbstract
{

    public class EfDataAccess<TContext> : IGenericDataAccess where TContext : DbContext, new()
    {

        private TContext _entities;

        protected IDbFactory<TContext> DbFactory
        {
            get;
            private set;
        }
        protected TContext DbContext
        {
            get { return _entities ?? (_entities = DbFactory.Init()); }
        }
        public EfDataAccess(IDbFactory<TContext> dbFactory)
        {
            DbFactory = dbFactory;
        }
        //private TContext _entities = new TContext();
        //public TContext Context
        //{

        //    get { return _entities; }
        //    set { _entities = value; }
        //}
        public int ExecuteNonQuery(string query, object parameters, string type = null)
        {

            return DbContext.Database.ExecuteSqlCommand(query, parameters);
        }


        public IEnumerable<TResult> ExecuteReader<TResult>(string query, object parameters, string type = null)
        {

            //return _entities.Database.SqlQuery<TResult>(query, parameters);
            return DbContext.Database.SqlQuery<TResult>(query, parameters);
        }
        public TResult ExecuteScalar<TResult>(string query, object parameters)
        {

            //return _entities.Database.SqlQuery<TResult>(query, parameters).SingleOrDefault();
            return DbContext.Database.SqlQuery<TResult>(query, parameters).SingleOrDefault();
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {

            // _entities.Set<TEntity>().Add(entity);
            DbContext.Set<TEntity>().Add(entity);
            //_entities.SaveChanges();
        }
        public void Save()
        {
            DbContext.SaveChanges();
        }


        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            //IQueryable<TEntity> query = _entities.Set<TEntity>();
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            return query;
        }

        public IQueryable<TEntity> FindBy<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>().Where(predicate);
            return query;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Edit<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }





        public IEnumerable<TResult> ExecuteReader<TResult, T>(string query, List<T> parameters, out List<object> returnobj, string type = null) where T : class
        {
            throw new NotImplementedException();
        }


        public int ExecuteNonQuery<T>(string query, List<T> parameters, out List<object> returnobj, string type = null)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<object> ExecuteReaderMul<T>(string query, List<T> parameters, out List<object> returnobj, string type = null) where T : class
        {
            throw new NotImplementedException();
        }
    }
}