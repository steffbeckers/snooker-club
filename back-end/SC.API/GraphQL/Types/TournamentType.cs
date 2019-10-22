using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;

namespace SC.API.GraphQL.Types
{
    public class TournamentType : ObjectGraphType<Tournament>
    {
        public TournamentType(TournamentRepository tournamentRepository, LeagueRepository leagueRepository, PlayerRepository playerRepository)
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name, nullable: true).Description("The name of the tournament");
            Field(x => x.DisplayName, nullable: true).Description("The display name of the tournament that should be used in app UI");
            Field(x => x.Date);

            Field(x => x.LeagueId, type: typeof(IdGraphType), nullable: true);
            Field<LeagueType>(
                "league",
                resolve: context =>
                {
                    if (context.Source.LeagueId != null)
                        return leagueRepository.GetById((Guid)context.Source.LeagueId);
                    return null;
                }
            );

            Field(x => x.WinnerId, type: typeof(IdGraphType), nullable: true);
            Field<PlayerType>(
                "winner",
                resolve: context =>
                {
                    if (context.Source.WinnerId != null)
                        return playerRepository.GetById((Guid)context.Source.WinnerId);
                    return null;
                }
            );

            Field(x => x.RunnerUpId, type: typeof(IdGraphType), nullable: true);
            Field<PlayerType>(
                "runnerUp",
                resolve: context =>
                {
                    if (context.Source.RunnerUpId != null)
                        return playerRepository.GetById((Guid)context.Source.RunnerUpId);
                    return null;
                }
            );

            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context => tournamentRepository.GetPlayersOfTournamentById(context.Source.Id)
            );

            Field<ListGraphType<PlayerPositionTournamentType>>(
               "playerPositions",
               resolve: context => tournamentRepository.GetPlayerPositionsOfTournamentById(context.Source.Id)
           );

            Field<ListGraphType<GroupType>>(
                "groups",
                resolve: context => tournamentRepository.GetGroupsOfTournamentById(context.Source.Id)
            );

            Field<ListGraphType<FrameType>>(
                "frames",
                resolve: context => tournamentRepository.GetFramesOfTournamentById(context.Source.Id)
            );
        }
    }
}
