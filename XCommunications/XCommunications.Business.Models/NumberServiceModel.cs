using System.ComponentModel.DataAnnotations;

namespace XCommunications.Business.Models
{
    public class NumberServiceModel
    {
        [Required]
        public int Id { get; set; }

        public bool Status { get; set; }

        [Required]
        [Range(100, 1000)]
        public int Cc { get; set; }

        [Required]
        [Range(100, 1000)]
        public int Ndc { get; set; }

        [Required]
        [Range(1000000, 10000000)]
        public int Sn { get; set; }

        public NumberServiceModel() { }
    }
}
