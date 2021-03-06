﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XCommunications.Data.Models
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
        [Range(1000, 10000)]
        public int Pin { get; set; }

        [Required]
        [Range(1000, 10000)]
        public int Puk { get; set; }

        [Required]
        public bool Status { get; set; }

        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
