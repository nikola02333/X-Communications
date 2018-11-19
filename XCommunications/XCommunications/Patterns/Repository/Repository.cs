using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommunications.Context;
using XCommunications.Interfaces;

namespace XCommunications.Patterns.Repository
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


        public void Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
