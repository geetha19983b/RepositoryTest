using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.GenericInterface;
using System.Data.Entity;

namespace DataAccess.GenericAbstract
{
    public  class EfRepositoryBase<TContext> :IGenericRepository where TContext : DbContext, new()
    {
        //protected DbContext Context;

        //public EfRepositoryBase(DbContext dbCtx)
        //{
        //    Context = dbCtx;
        //}
        private TContext _entities = new TContext();
        public TContext Context
        {

            get { return _entities; }
            set { _entities = value; }
        }
        public int ExecuteNonQuery(string query,object parameters,string type=null)
        {

            return _entities.Database.ExecuteSqlCommand(query,parameters);
        }


        public IEnumerable<TResult> ExecuteReader<TResult>(string query, object parameters, string type = null)
        {

            return _entities.Database.SqlQuery<TResult>(query, parameters);
        }
        public TResult ExecuteScalar<TResult>(string query, object parameters)
        {

            return _entities.Database.SqlQuery<TResult>(query, parameters).SingleOrDefault();
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {

            _entities.Set<TEntity>().Add(entity);
            //_entities.SaveChanges();
        }
        public void Save()
        {
            _entities.SaveChanges();
        }


        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>();
            return query;
        }

        public IQueryable<TEntity> FindBy<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>().Where(predicate);
            return query;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _entities.Set<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _entities.Set<TEntity>().Remove(entity);
        }

        public void Edit<TEntity>(TEntity entity) where TEntity : class
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

      
       
    }
}
