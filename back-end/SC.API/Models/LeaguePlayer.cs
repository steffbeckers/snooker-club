using System;

namespace SC.API.Models
{
    public class LeaguePlayer : SCModelBase
    {
        public int? Handicap { get; set; }

        public Guid LeagueId { get; set; }
        public League League { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
