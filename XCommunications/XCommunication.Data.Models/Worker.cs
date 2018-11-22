using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XCommunications.Data.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Contract = new HashSet<Contract>();
            RegistratedUser = new HashSet<RegistratedUser>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Operater { get; set; }

        public ICollection<Contract> Contract { get; set; }
        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
