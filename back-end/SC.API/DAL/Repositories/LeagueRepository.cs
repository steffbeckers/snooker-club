using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class LeagueRepository : Repository<League>
    {
        private new readonly SCContext context;

        public LeagueRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public IEnumerable<Player> GetPlayersOfLeagueById(Guid id)
        {
            return this.context.LeaguePlayer
                .Include(lp => lp.Player)
                .Where(lp => lp.LeagueId == id)
                .Select(lp => lp.Player)
                .ToList();
        }
    }
}
