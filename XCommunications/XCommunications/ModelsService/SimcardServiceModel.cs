using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsService
{
    public class SimcardServiceModel
    {
        public int Imsi { get; set; }
        public int Iccid { get; set; }
        public int Pin { get; set; }
        public int Puk { get; set; }
        public bool Status { get; set; }

        public SimcardServiceModel() { }
    }
}
