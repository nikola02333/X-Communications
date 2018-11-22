using System.Collections.Generic;

namespace XCommunications.Business.Interfaces
{
    public interface IService<T> where T : class
    {
        bool Add(T entity);

        bool Delete(int id);

        T Get(int id);

        IEnumerable<T> GetAll();

        bool Update(T worker);
    }
}
