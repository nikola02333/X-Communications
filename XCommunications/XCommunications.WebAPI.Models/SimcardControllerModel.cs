﻿using System.ComponentModel.DataAnnotations;

namespace XCommunications.WebAPI.Models
{
    public class SimcardControllerModel
    {
        [Required]
        public int Imsi { get; set; }

        [Required]
        public int Iccid { get; set; }

        [Required]
        [Range(100, 1000)]
        public int Pin { get; set; }

        [Required]
        [Range(100, 1000)]
        public int Puk { get; set; }

        public SimcardControllerModel() { }
    }
}