using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class Group : SCModelBase
    {
        public Group()
        {
            this.GroupPlayer = new List<GroupPlayer>();
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }

        public Guid TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public IList<GroupPlayer> GroupPlayer { get; set; }
    }
}
