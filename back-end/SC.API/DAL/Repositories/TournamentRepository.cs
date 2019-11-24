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

        public Tournament GetWithPlayersById(Guid id)
        {
            return this.context.Tournaments
                .Include(t => t.PlayerTournament)
                .ThenInclude(pt => pt.Player)
                .SingleOrDefault(t => t.Id == id);
        }

        public Tournament GetWithPlayerPositionsById(Guid id)
        {
            return this.context.Tournaments
                .Include(t => t.PlayerPositionTournament)
                .SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Tournament> GetByLeagueId(Guid leagueId)
        {
            return this.context.Tournaments.Where(t => t.LeagueId == leagueId)
                                           .OrderByDescending(d => d.Date)
                                           .ToList();
        }

        public IEnumerable<Player> GetPlayersOfTournamentById(Guid id)
        {
            Tournament tournament = this.context.Tournaments.Find(id);
            if (tournament == null)
            {
                return null;
            }

            List<PlayerTournament> playerTournaments = this.context.PlayerTournament
                .Include(pt => pt.Player)
                .Where(pt => pt.TournamentId == id)
                .ToList();

            return playerTournaments.Select(pt =>
            {
                Player player = pt.Player;
                player.Handicap = pt.Handicap;

                return player;
            });
        }

        public IEnumerable<PlayerPositionTournament> GetPlayerPositionsOfTournamentById(Guid id)
        {
            return this.context.PlayerPositionTournament
                .Include(ppt => ppt.Player)
                .Where(ppt => ppt.TournamentId == id)
                .OrderBy(ppt => ppt.Position)
                .ToList();
        }

        public IEnumerable<Group> GetGroupsOfTournamentById(Guid id)
        {
            return this.context.Groups
                .Where(g => g.TournamentId == id)
                .ToList();
        }

        public IEnumerable<Frame> GetFramesOfTournamentById(Guid id)
        {
            return this.context.Frames
                .Where(f => f.TournamentId == id)
                .OrderByDescending(f => f.EndDate)
                .ToList();
        }
    }
}
