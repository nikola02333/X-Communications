using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XCommunications.Data.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Contract = new HashSet<Contract>();
            RegistratedUser = new HashSet<RegistratedUser>();
        }

        [Required]
        [MinLength(13)]
        public int Id { get; set; }             

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public ICollection<Contract> Contract { get; set; }
        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
