using System;
using System.Collections.Generic;

namespace XCommunications.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // create object and add to db
        void Add(T entity);

        // read object from db with provided id
        T Get(T id);

        // read all objects from db
        IEnumerable<T> GetAll();

        // return object from db based on given id
        T GetById(int id);

        // update entity from db
        void Update(T entity);

        // deletes entity from db
        void Remove(T entity);

        // returns all objects from db which correspond to given predicate
        T Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
