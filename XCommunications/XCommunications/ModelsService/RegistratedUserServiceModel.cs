using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsService
{
    public class RegistratedUserServiceModel
    {
        public int Id { get; set; }
        public int Imsi { get; set; }
        public int CustomerId { get; set; }
        public int WorkerId { get; set; }
        public int NumberId { get; set; }

        public RegistratedUserServiceModel() { }
    }
}
