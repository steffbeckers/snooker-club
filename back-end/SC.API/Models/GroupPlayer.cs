using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class GroupPlayer : SCModelBase
    {
        public Guid GroupId { get; set; }
        public Guid PlayerId { get; set; }
        public int Position { get; set; }
    }
}
