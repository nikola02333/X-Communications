using System.Collections.Generic;
using XCommunications.ModelsController;
using XCommunications.ModelsService;

namespace XCommunications.Interfaces
{
    public interface IContractsService
    {
        void Add(ContractServiceModel contract);
        bool Delete(int id);
        ContractControllerModel Get(int id);
        IEnumerable<ContractServiceModel> GetAll();
        bool Put(ContractServiceModel contract);
    }
}