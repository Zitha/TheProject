using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProject.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is " +
                                            "required to use this repository.", "context");
            }

            Context = context;
            DBSet = Context.Set<T>();
        }

        protected DbSet<T> DBSet { get; set; }
        protected DbContext Context { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DBSet;
        }

        public virtual T GetById(int id)
        {
            return DBSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                DBSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                DBSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Detach(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry entry = Context.Entry(entity);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                DBSet.Attach(entity);
                DBSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            T entity = GetById(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        public DbRawSqlQuery<T> ExecuteStoredProc(string storeProName)
        {
            var results = Context.Database.SqlQuery<T>(storeProName);

            return results;
        }
    }
}
