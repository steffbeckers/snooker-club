using GraphQL.Types;
using SC.API.BLL;
using SC.API.GraphQL.Types;
using SC.API.Models;
using System;

namespace SC.API.GraphQL
{
    public class SCMutation : ObjectGraphType
    {
        public SCMutation(
            LeagueBLL leagueBLL,
            PlayerBLL playerBLL,
            SettingBLL settingBLL
        )
        {
            #region Leagues

            FieldAsync<LeagueType>(
                "createLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeagueInputType>>
                    {
                        Name = "league"
                    }
                ),
                resolve: async context =>
                {
                    League league = context.GetArgument<League>("league");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.CreateLeagueAsync(league)
                    );
                }
            );

            FieldAsync<LeagueType>(
                "updateLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<LeagueInputType>>
                    {
                        Name = "league"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");
                    League league = context.GetArgument<League>("league");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.UpdateLeagueAsync(id, league)
                    );
                }
            );

            FieldAsync<LeagueType>(
                "linkPlayerToLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeaguePlayerInputType>>
                    {
                        Name = "leaguePlayer"
                    }
                ),
                resolve: async context =>
                {
                    LeaguePlayer leaguePlayer = context.GetArgument<LeaguePlayer>("leaguePlayer");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.LinkPlayerToLeagueAsync(leaguePlayer)
                    );
                }
            );

            FieldAsync<LeagueType>(
                "unlinkPlayerFromLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LeaguePlayerInputType>>
                    {
                        Name = "leaguePlayer"
                    }
                ),
                resolve: async context =>
                {
                    LeaguePlayer leaguePlayer = context.GetArgument<LeaguePlayer>("leaguePlayer");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.UnlinkPlayerFromLeagueAsync(leaguePlayer)
                    );
                }
            );

            FieldAsync<BooleanGraphType>(
                "removeLeague",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await leagueBLL.RemoveLeagueAsync(id)
                    );
                }
            );

            #endregion

            #region Players

            FieldAsync<PlayerType>(
                "createPlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerInputType>>
                    {
                        Name = "player"
                    }
                ),
                resolve: async context =>
                {
                    Player player = context.GetArgument<Player>("player");

                    return await context.TryAsyncResolve(
                        async c => await playerBLL.CreatePlayerAsync(player)
                    );
                }
            );

            FieldAsync<PlayerType>(
                "updatePlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<PlayerInputType>>
                    {
                        Name = "player"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");
                    Player player = context.GetArgument<Player>("player");

                    return await context.TryAsyncResolve(
                        async c => await playerBLL.UpdatePlayerAsync(id, player)
                    );
                }
            );

            FieldAsync<BooleanGraphType>(
                "removePlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await playerBLL.RemovePlayerAsync(id)
                    );
                }
            );

            #endregion

            #region Settings

            FieldAsync<SettingType>(
                "createSetting",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SettingInputType>>
                    {
                        Name = "setting"
                    }
                ),
                resolve: async context =>
                {
                    Setting setting = context.GetArgument<Setting>("setting");

                    return await context.TryAsyncResolve(
                        async c => await settingBLL.CreateSettingAsync(setting)
                    );
                }
            );

            FieldAsync<SettingType>(
                "updateSetting",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    },
                    new QueryArgument<NonNullGraphType<SettingInputType>>
                    {
                        Name = "setting"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");
                    Setting setting = context.GetArgument<Setting>("setting");

                    return await context.TryAsyncResolve(
                        async c => await settingBLL.UpdateSettingAsync(id, setting)
                    );
                }
            );

            FieldAsync<BooleanGraphType>(
                "removeSetting",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await settingBLL.RemoveSettingAsync(id)
                    );
                }
            );

            #endregion
        }
    }
}
