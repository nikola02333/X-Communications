﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommunications.ModelsService
{
    public class CustomerServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public CustomerServiceModel() { }
    }
}
