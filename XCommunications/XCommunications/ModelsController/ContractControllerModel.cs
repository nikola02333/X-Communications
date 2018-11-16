using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsController
{
    public class ContractControllerModel
    {
        public int CustomerId { get; set; }
        public int WorkerId { get; set; }
        public string Tarif { get; set; }

        public ContractControllerModel() { }
    }
}
