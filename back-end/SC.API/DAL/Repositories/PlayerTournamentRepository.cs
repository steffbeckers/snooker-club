using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class PlayerTournamentRepository : Repository<PlayerTournament>
    {
        private new readonly SCContext context;

        public PlayerTournamentRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public PlayerTournament GetByPlayerAndTournamentId(Guid playerId, Guid tournamentId)
        {
            return this.context.PlayerTournament
                .Where(pt => pt.PlayerId == playerId && pt.TournamentId == tournamentId)
                .SingleOrDefault();
        }
    }
}
