using System.ComponentModel.DataAnnotations;

namespace XCommunications.Data.Models
{
    public partial class RegistratedUser
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Imsi { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public int NumberId { get; set; }

        public Customer Customer { get; set; }
        public Simcard ImsiNavigation { get; set; }
        public Number Number { get; set; }
        public Worker Worker { get; set; }
    }
}
