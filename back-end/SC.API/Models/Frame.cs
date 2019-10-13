using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class Frame : SCModelBase
    {
        public Frame()
        {
            this.FramePlayer = new List<FramePlayer>();
            this.Breaks = new List<Break>();
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid? TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public Guid? GroupId { get; set; }
        public Group Group { get; set; }

        public Guid? WinnerId { get; set; }
        public Player Winner { get; set; }

        public IList<FramePlayer> FramePlayer { get; set; }
        public IList<Break> Breaks { get; set; }
    }
}
