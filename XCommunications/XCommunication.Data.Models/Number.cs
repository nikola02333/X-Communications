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
        [MinLength(3)]
        public int Cc { get; set; }

        [Required]
        [MinLength(3)]
        public int Ndc { get; set; }

        [Required]
        public int Sn { get; set; }

        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
