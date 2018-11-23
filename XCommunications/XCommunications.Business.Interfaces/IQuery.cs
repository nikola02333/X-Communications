using System;
using System.Collections.Generic;

namespace XCommunications.Business.Interfaces
{
    public interface IQuery<T> where T : class
    {
        IEnumerable<T> FindAvailable();
    }
}
