using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;

namespace SC.API.GraphQL.Types
{
    public class PlayerPositionTournamentType : ObjectGraphType<PlayerPositionTournament>
    {
        public PlayerPositionTournamentType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Position, type: typeof(IntGraphType));
            Field(x => x.PlayerId, type: typeof(IdGraphType));
            Field(x => x.TournamentId, type: typeof(IdGraphType));
        }
    }
}
