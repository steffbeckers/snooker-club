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
            List<Player> players = this.context.LeaguePlayer
                .Include(lp => lp.Player)
                .Where(lp => lp.LeagueId == id)
                .Select(lp => lp.Player)
                .OrderBy(p => p.FirstName)
                .ToList();

            // Include handicaps from LeaguePlayer
            foreach (Player player in players)
            {
                player.Handicap = this.context.LeaguePlayer
                    .Single(lp => lp.LeagueId == id && lp.PlayerId == player.Id)
                    .Handicap;
            }

            return players;
        }
    }
}
