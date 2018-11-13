using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace XCommunications.Patterns.Repository
{
    public interface IRepository<T> where T:class
    {
        // create object to db
        void Add(T entity);

        // read object with provided id
        T Get(T id);

        // read all objects
        IEnumerable<T> GetAll();

        // update entity from db
        void Update(T entity);

        // deletes entity from db
        void Remove(T entity);
    }
}
