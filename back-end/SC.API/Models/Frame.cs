using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class Frame : SCModelBase
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? WinnerId { get; set; }
    }

    public class FrameType : ObjectGraphType<Frame>
    {
        public FrameType()
        {
            Field(x => x.Id);
            Field(x => x.StartDate);
            Field(x => x.EndDate);
            Field(x => x.WinnerId);
        }
    }
}
