using GraphQL.Types;
using SC.API.Models;

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
