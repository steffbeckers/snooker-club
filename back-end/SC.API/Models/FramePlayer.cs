using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class FramePlayer : SCModelBase
    {
        public Guid FrameId { get; set; }
        public Frame Frame { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }

        public int Position { get; set; }
        public int? Score { get; set; }
    }
}
