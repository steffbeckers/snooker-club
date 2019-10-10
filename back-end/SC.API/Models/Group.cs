using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class Group : SCModelBase
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Guid TournamentId { get; set; }
    }
}
