using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XCommunications.Data.Models
{
    public partial class Number
    {
        public Number()
        {
            RegistratedUser = new HashSet<RegistratedUser>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [Range(100, 10000)]
        public int Cc { get; set; }

        [Required]
        [Range(100, 10000)]
        public int Ndc { get; set; }

        [Required]
        [Range(1000000, 10000000)]
        public int Sn { get; set; }

        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
