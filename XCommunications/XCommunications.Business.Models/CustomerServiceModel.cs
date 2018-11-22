using System.ComponentModel.DataAnnotations;

namespace XCommunications.Business.Models
{
    public class CustomerServiceModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public CustomerServiceModel() { }
    }
}
