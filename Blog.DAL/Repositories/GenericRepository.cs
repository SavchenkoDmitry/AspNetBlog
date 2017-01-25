using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DAL.EF;

namespace Blog.DAL.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        public GenericRepository(ApplicationContext context)
        {
            Context = context;
        }

        private ApplicationContext _entities;
        public ApplicationContext Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public T FindById(int id)
        {
            T item = _entities.Set<T>().Find(id);
            return item;
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}
