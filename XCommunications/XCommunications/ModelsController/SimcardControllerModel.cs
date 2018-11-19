using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsController
{
    public class SimcardControllerModel
    {
        //[Required]
        public int Imsi { get; set; }

        //[Required]
        public int Iccid { get; set; }

        //[Required]
        //[MinLength(4)]
        public int Pin { get; set; }

        //[Required]
        //[MinLength(4)]
        public int Puk { get; set; }

        public SimcardControllerModel() { }
    }
}
