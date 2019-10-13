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
            this.LeaguePlayer = new List<LeaguePlayer>();
            this.Tournaments = new List<Tournament>();
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Season { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IList<LeaguePlayer> LeaguePlayer { get; set; }
        public IList<Tournament> Tournaments { get; set; }
    }
}
