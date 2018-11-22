using System;
using System.Collections.Generic;

namespace XCommunications.Business.Interfaces
{
    public interface IQuery<T> where T : class
    {
        // returns all objects  which correspond to given predicate
        IEnumerable<T> FindAvailable();
    }
}
