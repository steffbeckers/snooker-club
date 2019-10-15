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
