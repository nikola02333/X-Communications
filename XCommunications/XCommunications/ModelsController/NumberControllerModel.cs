using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsController
{
    public class NumberControllerModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(100, 1000)]
        public int Cc { get; set; }

        [Required]
        [Range(100, 1000)]
        public int Ndc { get; set; }

        [Required]
        [Range(1000000, 10000000)]
        public int Sn { get; set; }

        public bool Status { get; set; }

        public NumberControllerModel() { }
    }
}
