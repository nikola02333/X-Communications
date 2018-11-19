using System.Collections.Generic;
using XCommunications.ModelsController;
using XCommunications.ModelsService;

namespace XCommunications.Interfaces
{
    public interface IRegistratedUsersService
    {
        void Add(RegistratedUserServiceModel user);
        bool Delete(int id);
        RegistratedUserControllerModel Get(int id);
        IEnumerable<RegistratedUserServiceModel> GetAll();
        bool Put(RegistratedUserServiceModel user);
    }
}