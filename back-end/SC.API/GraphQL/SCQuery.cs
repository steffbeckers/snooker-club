using GraphQL.Types;
using SC.API.DAL;
using SC.API.DAL.Repositories;
using SC.API.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL
{
    public class SCQuery : ObjectGraphType
    {
        public SCQuery(
            LeagueRepository leagueRepository,
            PlayerRepository playerRepository,
            TournamentRepository tournamentRepository,
            UserRepository userRepository
        )
        {
            // Leagues
            Field<ListGraphType<LeagueType>>(
                "leagues",
                resolve: context => leagueRepository.Get()
            );
            Field<LeagueType>(
                "league",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => leagueRepository.GetById(context.GetArgument<Guid>("id"))
            );

            // Players
            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context => playerRepository.Get()
            );
            Field<PlayerType>(
                "player",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => playerRepository.GetById(context.GetArgument<Guid>("id"))
            );

            // Tournaments
            Field<ListGraphType<TournamentType>>(
                "tournaments",
                resolve: context => tournamentRepository.Get()
            );
            Field<TournamentType>(
                "tournament",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => tournamentRepository.GetById(context.GetArgument<Guid>("id"))
            );

            // Users
            // TODO: Add authorization on user retrieval?
            Field<ListGraphType<UserType>>(
                "users",
                resolve: context => userRepository.Get()
            );
            Field<LeagueType>(
                "user",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => userRepository.GetById(context.GetArgument<Guid>("id"))
            );
        }
    }
}
