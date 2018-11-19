using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsController
{
    public class RegistratedUserControllerModel
    {
        //[Required]
        public int Id { get; set; }

        //[Required]
        public int Imsi { get; set; }

        //[Required]
        public int CustomerId { get; set; }

        //[Required]
        public int WorkerId { get; set; }

        //[Required]
        public int NumberId { get; set; }

        public RegistratedUserControllerModel() { }
    }
}
