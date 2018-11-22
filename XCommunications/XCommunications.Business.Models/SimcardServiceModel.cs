using System.ComponentModel.DataAnnotations;

namespace XCommunications.Business.Models
{
    public class SimcardServiceModel
    {
        [Required]
        public int Imsi { get; set; }

        [Required]
        public int Iccid { get; set; }

        [Required]
        [Range(1000,10000)]
        public int Pin { get; set; }

        [Required]
        [Range(1000, 10000)]
        public int Puk { get; set; }

        public bool Status { get; set; }

        public SimcardServiceModel() { }
    }
}
