using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL.Types
{
    public class TournamentType : ObjectGraphType<Tournament>
    {
        public TournamentType(TournamentRepository tournamentRepository, LeagueRepository leagueRepository)
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name, nullable: true).Description("The name of the tournament");
            Field(x => x.DisplayName, nullable: true).Description("The display name of the tournament that should be used in app UI");
            Field(x => x.Date);

            Field(x => x.LeagueId, type: typeof(IdGraphType), nullable: true);
            Field<LeagueType>(
                "league",
                resolve: context => {
                    if (context.Source.LeagueId != null)
                        return leagueRepository.GetById((Guid)context.Source.LeagueId);
                    return null;
                }
            );

            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context => tournamentRepository.GetPlayersOfTournamentById(context.Source.Id)
            );
        }
    }
}
