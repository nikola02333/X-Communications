﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XCommunications.ModelsDB
{
    public partial class Contract
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

        public Customer Customer { get; set; }
        public Worker Worker { get; set; }
    }
}
