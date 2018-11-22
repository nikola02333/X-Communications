using System.ComponentModel.DataAnnotations;

namespace XCommunications.WebAPI.Models
{
    public class ContractControllerModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public string Tarif { get; set; }

        public ContractControllerModel() { }
    }
}
