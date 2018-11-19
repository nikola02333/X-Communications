using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XCommunications.ModelsDB
{
    public partial class Simcard
    {
        public Simcard()
        {
            RegistratedUser = new HashSet<RegistratedUser>();
        }

        [Required]
        public int Imsi { get; set; }

        [Required]
        public int Iccid { get; set; }

        [Required]
        [MinLength(4)]
        public int Pin { get; set; }

        [Required]
        [MinLength(4)]
        public int Puk { get; set; }

        [Required]
        public bool Status { get; set; }

        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
