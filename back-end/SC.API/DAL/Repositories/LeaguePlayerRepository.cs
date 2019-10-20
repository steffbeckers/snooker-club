using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class LeaguePlayerRepository : Repository<LeaguePlayer>
    {
        private new readonly SCContext context;

        public LeaguePlayerRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public LeaguePlayer GetByLeagueAndPlayerId(Guid leagueId, Guid playerId)
        {
            return this.context.LeaguePlayer
                .Where(lp => lp.LeagueId == leagueId && lp.PlayerId == playerId)
                .SingleOrDefault();
        }
    }
}
