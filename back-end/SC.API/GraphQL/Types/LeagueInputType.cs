using GraphQL.Types;

namespace SC.API.GraphQL.Types
{
    public class LeagueInputType : InputObjectGraphType
    {
        public LeagueInputType()
        {
            Name = "leagueInput";
            Field<NonNullGraphType<StringGraphType>>("displayName");
            Field<StringGraphType>("season");
            Field<DateGraphType>("startDate");
            Field<DateGraphType>("endDate");
        }
    }
}
