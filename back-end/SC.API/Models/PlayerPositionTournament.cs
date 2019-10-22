using System;

namespace SC.API.Models
{
    public class PlayerPositionTournament : SCModelBase
    {
        public int Position { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }

        public Guid TournamentId { get; set; }
        public Tournament Tournament { get; set; }
    }
}
