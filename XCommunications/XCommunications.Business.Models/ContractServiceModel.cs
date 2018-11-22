using System;
using System.ComponentModel.DataAnnotations;

namespace XCommunications.Business.Models
{
    public class ContractServiceModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public string Tarif { get; set; }

        public ContractServiceModel() { }
    }
}
