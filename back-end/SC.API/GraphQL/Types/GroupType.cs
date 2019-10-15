using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;

namespace SC.API.GraphQL.Types
{
    public class GroupType : ObjectGraphType<Group>
    {
        public GroupType(GroupRepository groupRepository, TournamentRepository tournamentRepository)
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name, nullable: true).Description("The name of the group");
            Field(x => x.DisplayName, nullable: true).Description("The display name of the group that should be used in app UI");

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

            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context => groupRepository.GetPlayersOfGroupById(context.Source.Id)
            );

            Field<ListGraphType<FrameType>>(
                "frames",
                resolve: context => groupRepository.GetFramesOfGroupById(context.Source.Id)
            );
        }
    }
}
