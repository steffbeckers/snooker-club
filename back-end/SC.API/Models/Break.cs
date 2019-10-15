using System;

namespace SC.API.Models
{
    public class Break : SCModelBase
    {
        public int Score { get; set; }

        public Guid FrameId { get; set; }
        public Frame Frame { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
