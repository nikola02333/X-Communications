using System.Collections.Generic;
using XCommunications.ModelsController;
using XCommunications.ModelsService;

namespace XCommunications.Interfaces
{
    public interface IWorkersService
    {
        void Add(WorkerServiceModel worker);
        bool Delete(int id);
        WorkerControllerModel Get(int id);
        IEnumerable<WorkerServiceModel> GetAll();
        bool Put(WorkerServiceModel worker);
    }
}