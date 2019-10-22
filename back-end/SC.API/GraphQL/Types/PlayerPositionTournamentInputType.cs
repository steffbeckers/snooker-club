using GraphQL.Types;

namespace SC.API.GraphQL.Types
{
    public class PlayerPositionTournamentInputType : InputObjectGraphType
    {
        public PlayerPositionTournamentInputType()
        {
            Name = "playerPositionTournamentInput";
            Field<IdGraphType>("id");
            Field<NonNullGraphType<IntGraphType>>("position");
            Field<IdGraphType>("playerId"); // Nullable for unlink
            Field<NonNullGraphType<IdGraphType>>("tournamentId");
        }
    }
}
