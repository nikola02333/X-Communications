using System.Collections.Generic;
using XCommunications.ModelsController;
using XCommunications.ModelsService;

namespace XCommunications.Interfaces
{
    public interface ISimcardsService
    {
        void Add(SimcardServiceModel sim);
        bool Delete(int id);
        SimcardControllerModel Get(int id);
        IEnumerable<SimcardServiceModel> GetAll();
        bool Put(SimcardServiceModel sim);
    }
}