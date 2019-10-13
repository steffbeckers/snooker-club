using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL.Types
{
    public class SettingInputType : InputObjectGraphType
    {
        public SettingInputType()
        {
            Name = "settingInput";
            Field<NonNullGraphType<StringGraphType>>("key");
            Field<StringGraphType>("value");
        }
    }
}
