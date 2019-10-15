using System;

namespace SC.API.Models
{
    public class LeaguePlayer : SCModelBase
    {
        public Guid LeagueId { get; set; }
        public League League { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }

        public int? Handicap { get; set; }
    }
}
