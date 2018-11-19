using System.Collections.Generic;
using XCommunications.ModelsController;
using XCommunications.ModelsService;

namespace XCommunications.Interfaces
{
    public interface ICustomersService
    {
        void Add(CustomerServiceModel customer);
        bool Delete(int id);
        CustomerControllerModel Get(int id);
        IEnumerable<CustomerServiceModel> GetAll();
        bool Put(CustomerServiceModel customer);
    }
}