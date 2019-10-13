﻿using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class FrameRepository : Repository<Frame>
    {
        private new readonly SCContext context;

        public FrameRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public IEnumerable<Player> GetPlayersOfFrameById(Guid id)
        {
            Frame frame = this.context.Frames.Find(id);
            if (frame == null)
            {
                return null;
            }

            List<Player> players = this.context.FramePlayer
                .Include(fp => fp.Player)
                .Where(fp => fp.FrameId == id)
                .Select(fp => fp.Player)
                .ToList();

            foreach (Player player in players)
            {
                // Include positions and scores from FramePlayer
                FramePlayer framePlayer = this.context.FramePlayer
                    .Single(fp => fp.FrameId == id && fp.PlayerId == player.Id);
                player.Position = framePlayer.Position;
                player.Score = framePlayer.Score;

                // Include handicaps from PlayerTournament
                player.Handicap = this.context.PlayerTournament
                    .Single(pt => pt.TournamentId == frame.TournamentId && pt.PlayerId == player.Id)
                    .Handicap;
            }

            return players;
        }

        public IEnumerable<Break> GetBreaksOfFrameById(Guid id)
        {
            Frame frame = this.context.Frames.Find(id);
            if (frame == null)
            {
                return null;
            }

            List<Break> breaks = this.context.Breaks
                .Where(f => f.FrameId == id)
                .ToList();

            return breaks;
        }
    }
}