using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class Setting : SCModelBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
