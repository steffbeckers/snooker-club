using GraphQL.Types;

namespace SC.API.GraphQL.Types
{
    public class PlayerTournamentInputType : InputObjectGraphType
    {
        public PlayerTournamentInputType()
        {
            Name = "playerTournamentInput";
            Field<NonNullGraphType<IdGraphType>>("playerId");
            Field<NonNullGraphType<IdGraphType>>("tournamentId");
            Field<IntGraphType>("handicap");
        }
    }
}
