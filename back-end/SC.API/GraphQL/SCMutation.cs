using GraphQL.Types;
using SC.API.BLL;
using SC.API.DAL.Repositories;
using SC.API.GraphQL.Types;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL
{
    public class SCMutation : ObjectGraphType
    {
        public SCMutation(SettingBLL settingBLL)
        {
            #region Settings

            FieldAsync<SettingType>(
                "createSetting",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<SettingInputType>> {
                    Name = "setting"
                }),
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
