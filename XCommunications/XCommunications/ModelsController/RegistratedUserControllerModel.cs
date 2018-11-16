using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsController
{
    public class RegistratedUserControllerModel
    {
        public int Imsi { get; set; }
        public int CustomerId { get; set; }
        public int WorkerId { get; set; }
        public int NumberId { get; set; }

        public RegistratedUserControllerModel() { }
    }
}
