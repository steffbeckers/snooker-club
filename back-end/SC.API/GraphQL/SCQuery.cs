using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.GraphQL.Types;
using System;
using System.Linq;

namespace SC.API.GraphQL
{
    public class SCQuery : ObjectGraphType
    {
        public SCQuery(
            LeagueRepository leagueRepository,
            PlayerRepository playerRepository,
            TournamentRepository tournamentRepository,
            UserRepository userRepository,
            SettingRepository settingRepository
        )
        {
            // Leagues
            Field<ListGraphType<LeagueType>>(
                "leagues",
                resolve: context => leagueRepository.Get(null, p => p.OrderBy(p => p.DisplayName))
            );
            Field<LeagueType>(
                "league",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => leagueRepository.GetById(context.GetArgument<Guid>("id"))
            );

            // Players
            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context => playerRepository.Get(null, p => p.OrderBy(p => p.FirstName))
            );
            Field<PlayerType>(
                "player",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => playerRepository.GetById(context.GetArgument<Guid>("id"))
            );

            // Tournaments
            Field<ListGraphType<TournamentType>>(
                "tournaments",
                resolve: context => tournamentRepository.Get(null, t => t.OrderByDescending(t => t.Date))
            );
            Field<TournamentType>( 
                "tournament",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => tournamentRepository.GetById(context.GetArgument<Guid>("id"))
            );

            // Users
            Field<ListGraphType<UserType>>(
                "users",
                resolve: context =>
                {
                    // TODO
                    //ClaimsPrincipal user = (ClaimsPrincipal)context.UserContext;
                    //if (!user.IsInRole("Admin")) { return null; }

                    return userRepository.Get();
                }
            );
            Field<UserType>(
                "user",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    // TODO
                    //ClaimsPrincipal user = (ClaimsPrincipal)context.UserContext;
                    //if (!user.IsInRole("Admin")) { return null; }

                    return userRepository.GetById(context.GetArgument<Guid>("id"));
                }
            );

            // Settings
            Field<ListGraphType<SettingType>>(
                "settings",
                resolve: context => settingRepository.Get(null, s => s.OrderByDescending(s => s.Key))
            );
            Field<SettingType>(
                "setting",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "key" }),
                resolve: context => settingRepository.GetByKey(context.GetArgument<string>("key"))
            );
        }
    }
}
