using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class PlayerPositionTournamentRepository : Repository<PlayerPositionTournament>
    {
        private new readonly SCContext context;

        public PlayerPositionTournamentRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public PlayerPositionTournament GetByPositionAndTournamentId(int position, Guid tournamentId)
        {
            return this.context.PlayerPositionTournament
                .Where(ppt => ppt.Position == position && ppt.TournamentId == tournamentId)
                .SingleOrDefault();
        }

        public void DeleteByPlayerAndTournamentId(Guid playerId, Guid tournamentId)
        {
            List<PlayerPositionTournament> playerPositionTournaments = this.context.PlayerPositionTournament
                .Where(ppt => ppt.PlayerId == playerId && ppt.TournamentId == tournamentId)
                .ToList();

            this.context.RemoveRange(playerPositionTournaments);
            this.context.SaveChanges();
        }
    }
}
