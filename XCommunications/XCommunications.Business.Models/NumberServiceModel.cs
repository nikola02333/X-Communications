using System.ComponentModel.DataAnnotations;

namespace XCommunications.Business.Models
{
    public class NumberServiceModel
    {
        [Required]
        public int Id { get; set; }

        public bool Status { get; set; }

        [Required]
        [MinLength(3)]
        public int Cc { get; set; }

        [Required]
        [MinLength(3)]
        public int Ndc { get; set; }

        [Required]
        public int Sn { get; set; }

        public NumberServiceModel() { }
    }
}
