using GraphQL.Types;

namespace SC.API.GraphQL.Types
{
    public class LeaguePlayerInputType : InputObjectGraphType
    {
        public LeaguePlayerInputType()
        {
            Name = "leaguePlayerInput";
            Field<NonNullGraphType<IdGraphType>>("leagueId");
            Field<NonNullGraphType<IdGraphType>>("playerId");
            Field<IntGraphType>("handicap");
        }
    }
}
