using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class Tournament : SCModelBase
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTime Date { get; set; }

        public Guid? LeagueId { get; set; }
        public League League { get; set; }
        
        public Guid? WinnerId { get; set; }
        public Player Winner { get; set; }

        public Guid? RunnerUpId { get; set; }
        public Player RunnerUp { get; set; }
    }
}
