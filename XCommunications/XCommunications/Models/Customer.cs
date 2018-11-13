using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Contract = new HashSet<Contract>();
            RegistratedUser = new HashSet<RegistratedUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public ICollection<Contract> Contract { get; set; }
        public ICollection<RegistratedUser> RegistratedUser { get; set; }
    }
}
