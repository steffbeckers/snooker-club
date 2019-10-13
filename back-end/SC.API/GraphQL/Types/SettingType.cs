using GraphQL.Types;
using SC.API.DAL.Repositories;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL.Types
{
    public class SettingType : ObjectGraphType<Setting>
    {
        public SettingType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Key).Description("The key of the setting");
            Field(x => x.Value, nullable: true).Description("The value of the setting");
        }
    }
}
