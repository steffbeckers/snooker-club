using GraphQL.Types;

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
