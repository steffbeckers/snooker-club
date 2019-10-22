using GraphQL.Types;

namespace SC.API.GraphQL.Types
{
    public class PlayerPositionTournamentInputType : InputObjectGraphType
    {
        public PlayerPositionTournamentInputType()
        {
            Name = "playerPositionTournamentInput";
            Field<NonNullGraphType<IdGraphType>>("id");
            Field<NonNullGraphType<IntGraphType>>("position");
            Field<NonNullGraphType<IdGraphType>>("playerId");
            Field<NonNullGraphType<IdGraphType>>("tournamentId");
        }
    }
}
