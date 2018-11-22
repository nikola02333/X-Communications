using System.ComponentModel.DataAnnotations;

namespace XCommunications.WebAPI.Models
{
    public class NumberControllerModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(100, 10000)]
        public int Cc { get; set; }

        [Required]
        [Range(100, 10000)]
        public int Ndc { get; set; }

        [Required]
        [Range(1000000, 10000000)]
        public int Sn { get; set; }

        public bool Status { get; set; }

        public NumberControllerModel() { }
    }
}
