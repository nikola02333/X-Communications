using System.Collections.Generic;
using XCommunications.ModelsController;
using XCommunications.ModelsService;

namespace XCommunications.Interfaces
{
    public interface INumbersService
    {
        void Add(NumberServiceModel number);
        bool Delete(int id);
        NumberControllerModel Get(int id);
        IEnumerable<NumberServiceModel> GetAll();
        bool Put(NumberServiceModel number);
    }
}