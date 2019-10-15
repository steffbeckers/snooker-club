using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SC.API.Models
{
    public class Player : SCModelBase
    {
        public Player()
        {
            this.LeaguePlayer = new List<LeaguePlayer>();
            this.PlayerTournament = new List<PlayerTournament>();
            this.GroupPlayer = new List<GroupPlayer>();
            this.FramePlayer = new List<FramePlayer>();
            this.Breaks = new List<Break>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public IList<LeaguePlayer> LeaguePlayer { get; set; }
        public IList<PlayerTournament> PlayerTournament { get; set; }
        public IList<GroupPlayer> GroupPlayer { get; set; }
        public IList<FramePlayer> FramePlayer { get; set; }
        public IList<Break> Breaks { get; set; }

        // Fields in many-to-many tables that get filled in the player
        [NotMapped]
        public int? Handicap { get; set; }
        [NotMapped]
        public int? Position { get; set; }
        [NotMapped]
        public int? Score { get; set; }
    }
}
