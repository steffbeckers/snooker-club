using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class Break : SCModelBase
    {
        public Guid FrameId { get; set; }
        public Guid PlayerId { get; set; }
        public int Score { get; set; }
    }

    public class BreakType : ObjectGraphType<Break>
    {
        public BreakType()
        {
            Field(x => x.Id);
            Field(x => x.FrameId);
            Field(x => x.PlayerId);
            Field(x => x.Score);
        }
    }
}
