using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class TournamentRepository : Repository<Tournament>
    {
        private new readonly SCContext context;

        public TournamentRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public IEnumerable<Tournament> GetByLeagueId (Guid leagueId)
        {
            return this.context.Tournaments.Where(a => a.LeagueId == leagueId).ToList();
        }

        public IEnumerable<Player> GetPlayersOfTournamentById(Guid id)
        {
            List<Player> players = this.context.PlayerTournament
                .Include(lp => lp.Player)
                .Where(lp => lp.TournamentId == id)
                .Select(lp => lp.Player)
                .ToList();

            // Include handicaps from PlayerTournament
            foreach (Player player in players)
            {
                player.Handicap = this.context.PlayerTournament
                    .Single(lp => lp.TournamentId == id && lp.PlayerId == player.Id).Handicap;
            }

            return players;
        }
    }
}
