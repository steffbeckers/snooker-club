using GraphQL.Types;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL.Types
{
    public class LeagueType : ObjectGraphType<League>
    {
        public LeagueType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.DisplayName);
            Field(x => x.Season);
            Field(x => x.StartDate, nullable: true);
            Field(x => x.EndDate, nullable: true);
        }
    }
}
