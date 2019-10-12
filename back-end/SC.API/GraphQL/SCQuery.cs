using GraphQL.Types;
using SC.API.DAL;
using SC.API.DAL.Repositories;
using SC.API.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL
{
    public class SCQuery : ObjectGraphType
    {
        public SCQuery(LeagueRepository leagueRepository)
        {
            Field<ListGraphType<LeagueType>>(
                "leagues",
                resolve: context => leagueRepository.GetAll()
            );
        }
    }
}
