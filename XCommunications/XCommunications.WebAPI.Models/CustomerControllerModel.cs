using System.ComponentModel.DataAnnotations;

namespace XCommunications.WebAPI.Models
{
    public class CustomerControllerModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public CustomerControllerModel() { }
    }
}
