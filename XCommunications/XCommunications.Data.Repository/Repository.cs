using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XCommunications.Data.Context;
using XCommunications.Data.Interfaces;

namespace XCommunications.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly XCommunicationsContext dbContext;

        public Repository(XCommunicationsContext context)
        {
            dbContext = context;
        }

        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public T Get(T id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public T Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return (T)dbContext.Set<T>().Where(predicate).FirstOrDefault();
        }
    }
}
