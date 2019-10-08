﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class League : SCModelBase
    {
        public string Name { get; set; }
        public string Season { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
