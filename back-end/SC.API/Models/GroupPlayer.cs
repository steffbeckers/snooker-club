using System;

namespace SC.API.Models
{
    public class GroupPlayer : SCModelBase
    {
        public int Position { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
