using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class League : SCModelBase
    {
        public League()
        {
            this.Tournaments = new List<Tournament>();
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Season { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<Tournament> Tournaments { get; set; }
    }
}
