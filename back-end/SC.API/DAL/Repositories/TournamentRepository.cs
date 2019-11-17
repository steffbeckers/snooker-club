using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<Player> players = this.context.PlayerTournament
                .Include(lp => lp.Player)
                .Where(lp => lp.TournamentId == id)
                .Select(lp => lp.Player)
                .OrderBy(p => p.FirstName)
                .ToList();

            // Include handicaps from PlayerTournament
            foreach (Player player in players)
            {
                player.Handicap = this.context.PlayerTournament
                    .Single(lp => lp.TournamentId == id && lp.PlayerId == player.Id)
                    .Handicap;
            }

            return players;
        }

        public IEnumerable<PlayerPositionTournament> GetPlayerPositionsOfTournamentById(Guid id)
        {
            List<PlayerPositionTournament> playerPositions = this.context.PlayerPositionTournament
                .Include(ppt => ppt.Player)
                .Where(ppt => ppt.TournamentId == id)
                .OrderBy(ppt => ppt.Position)
                .ToList();

            return playerPositions;
        }

        public IEnumerable<Group> GetGroupsOfTournamentById(Guid id)
        {
            List<Group> groups = this.context.Groups
                .Where(g => g.TournamentId == id)
                .ToList();

            return groups;
        }

        public IEnumerable<Frame> GetFramesOfTournamentById(Guid id)
        {
            List<Frame> frames = this.context.Frames
                .Where(f => f.TournamentId == id)
                .OrderByDescending(f => f.EndDate)
                .ToList();

            return frames;
        }
    }
}
