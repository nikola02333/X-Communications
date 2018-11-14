using System;
using System.Collections.Generic;

namespace XCommunications.Models
{
    public partial class Simcard
    {
        public Simcard()
        {
            RegistratedUser = new HashSet<RegistratedUser>();
        }

        public int Imsi { get; set; }
        public int Iccid { get; set; }
        public int Pin { get; set; }
        public int Puk { get; set; }
        public bool Status { get; set; }

        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
