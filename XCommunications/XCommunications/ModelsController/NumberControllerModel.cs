using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsController
{
    public class NumberControllerModel
    {
        //[Required]
        public int Id { get; set; }

        //[Required]
        //[MinLength(3)]
        public int Cc { get; set; }

        //[Required]
        //[MinLength(3)]
        public int Ndc { get; set; }

        //[Required]
        public int Sn { get; set; }

        public bool Status { get; set; }

        public NumberControllerModel() { }
    }
}
