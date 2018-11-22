using System.ComponentModel.DataAnnotations;

namespace XCommunications.WebAPI.Models
{
    public class WorkerControllerModel
    {
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


        public string Operater { get; set; }

        public WorkerControllerModel() { }
    }
}
