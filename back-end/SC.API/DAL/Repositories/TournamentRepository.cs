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
            return this.context.PlayerTournament
                .Include(pt => pt.Player)
                .Where(pt => pt.TournamentId == id)
                .Select(pt => pt.Player)
                .ToList();
        }
    }
}
