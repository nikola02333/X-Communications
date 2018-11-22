using System.ComponentModel.DataAnnotations;

namespace XCommunications.Business.Models
{
    public class RegistratedUserServiceModel
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

        public RegistratedUserServiceModel() { }
    }
}
