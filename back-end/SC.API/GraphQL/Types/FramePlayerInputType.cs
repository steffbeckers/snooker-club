using GraphQL.Types;

namespace SC.API.GraphQL.Types
{
    public class FramePlayerInputType : InputObjectGraphType
    {
        public FramePlayerInputType()
        {
            Name = "framePlayerInput";
            Field<IdGraphType>("frameId");
            Field<NonNullGraphType<IdGraphType>>("playerId");
            Field<NonNullGraphType<IntGraphType>>("position");
            Field<IntGraphType>("score");
        }
    }
}
