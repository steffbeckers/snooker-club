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

        public League GetWithPlayersById(Guid id)
        {
            return this.context.Leagues
                .Include(l => l.LeaguePlayer)
                .ThenInclude(lp => lp.Player)
                .SingleOrDefault(l => l.Id == id);
        }

        public IEnumerable<Player> GetPlayersOfLeagueById(Guid id)
        {
            List<LeaguePlayer> leaguePlayers = this.context.LeaguePlayer
                .Include(lp => lp.Player)
                .Where(lp => lp.LeagueId == id)
                .ToList();

            return leaguePlayers.Select(lp =>
            {
                Player player = lp.Player;
                player.Handicap = lp.Handicap;

                return player;
            });
        }
    }
}
