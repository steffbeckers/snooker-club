using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class GroupRepository : Repository<Group>
    {
        private new readonly SCContext context;

        public GroupRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public IEnumerable<Player> GetPlayersOfGroupById(Guid id)
        {
            Group group = this.context.Groups.Find(id);
            if (group == null)
            {
                return null;
            }

            List<Player> players = this.context.GroupPlayer
                .Include(gp => gp.Player)
                .Where(gp => gp.GroupId == id)
                .Select(gp => gp.Player)
                .ToList();

            foreach (Player player in players)
            {
                // Include positions from GroupPlayer
                player.Position = this.context.GroupPlayer
                    .Single(gp => gp.GroupId == id && gp.PlayerId == player.Id)
                    .Position;

                // Include handicaps from PlayerTournament
                player.Handicap = this.context.PlayerTournament
                    .Single(pt => pt.TournamentId == group.TournamentId && pt.PlayerId == player.Id)
                    .Handicap;
            }

            return players;
        }

        public IEnumerable<Frame> GetFramesOfGroupById(Guid id)
        {
            Group group = this.context.Groups.Find(id);
            if (group == null)
            {
                return null;
            }

            List<Frame> frames = this.context.Frames
                .Where(f => f.GroupId == id)
                .ToList();

            return frames;
        }
    }
}
