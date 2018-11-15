using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsService
{
    public class NumberServiceModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int Cc { get; set; }
        public int Ndc { get; set; }
        public int Sn { get; set; }

        public NumberServiceModel() { }
    }
}
