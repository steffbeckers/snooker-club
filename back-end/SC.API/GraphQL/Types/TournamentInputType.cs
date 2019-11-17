using GraphQL.Types;

namespace SC.API.GraphQL.Types
{
    public class TournamentInputType : InputObjectGraphType
    {
        public TournamentInputType()
        {
            Name = "tournamentInput";
            Field<NonNullGraphType<DateGraphType>>("date");
            Field<StringGraphType>("displayName");
            Field<IdGraphType>("leagueId");
            Field<IdGraphType>("winnerId");
            Field<IdGraphType>("runnerUpId");
        }
    }
}
