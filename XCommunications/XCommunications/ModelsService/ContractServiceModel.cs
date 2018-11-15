using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsService
{
    public class ContractServiceModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int WorkerId { get; set; }
        public string Tarif { get; set; }

        public ContractServiceModel() { }
    }
}
