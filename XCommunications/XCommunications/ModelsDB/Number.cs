using System;
using System.Collections.Generic;

namespace XCommunications.ModelsDB
{
    public partial class Number
    {
        public Number()
        {
            RegistratedUser = new HashSet<RegistratedUser>();
        }

        public int Id { get; set; }
        public bool Status { get; set; }
        public int Cc { get; set; }
        public int Ndc { get; set; }
        public int Sn { get; set; }

        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
