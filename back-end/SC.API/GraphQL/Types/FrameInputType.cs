using GraphQL.Types;

namespace SC.API.GraphQL.Types
{
    public class FrameInputType : InputObjectGraphType
    {
        public FrameInputType()
        {
            Name = "frameInput";
            Field<DateTimeGraphType>("startDate");
            Field<DateTimeGraphType>("endDate");
            Field<IdGraphType>("tournamentId");
            Field<IdGraphType>("groupId");
            Field<IdGraphType>("winnerId");
            Field<ListGraphType<FramePlayerInputType>>("framePlayer");
        }
    }
}
