using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;

namespace SC.API.GraphQL.Types
{
    public class FrameType : ObjectGraphType<Frame>
    {
        public FrameType(
            FrameRepository frameRepository,
            TournamentRepository tournamentRepository,
            GroupRepository groupRepository,
            PlayerRepository playerRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.StartDate, type: typeof(DateTimeGraphType));
            Field(x => x.EndDate, type: typeof(DateTimeGraphType));

            Field(x => x.TournamentId, type: typeof(IdGraphType), nullable: true);
            Field<TournamentType>(
                "tournament",
                resolve: context =>
                {
                    if (context.Source.TournamentId != null)
                        return tournamentRepository.GetById((Guid)context.Source.TournamentId);
                    return null;
                }
            );

            Field(x => x.GroupId, type: typeof(IdGraphType), nullable: true);
            Field<GroupType>(
                "group",
                resolve: context =>
                {
                    if (context.Source.GroupId != null)
                        return groupRepository.GetById((Guid)context.Source.GroupId);
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

            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context => frameRepository.GetPlayersOfFrameById(context.Source.Id)
            );

            Field<ListGraphType<BreakType>>(
                "breaks",
                resolve: context => frameRepository.GetBreaksOfFrameById(context.Source.Id)
            );
        }
    }
}
