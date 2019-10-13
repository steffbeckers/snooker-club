﻿using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Tournament> GetByLeagueId(Guid leagueId)
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
                    .Single(lp => lp.TournamentId == id && lp.PlayerId == player.Id)
                    .Handicap;
            }

            return players;
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
                .ToList();

            return frames;
        }
    }
}