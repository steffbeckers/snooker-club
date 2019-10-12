using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class LeagueRepository
    {
        private readonly SCContext context;

        public LeagueRepository(SCContext context)
        {
            this.context = context;
        }

        public IEnumerable<League> GetAll()
        {
            return this.context.Leagues;
        }
    }
}
