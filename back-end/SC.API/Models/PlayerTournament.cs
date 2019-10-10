using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class PlayerTournament : SCModelBase
    {
        public Guid PlayerId { get; set; }
        public Guid TournamentId { get; set; }
        public int? Handicap { get; set; }
    }
}
