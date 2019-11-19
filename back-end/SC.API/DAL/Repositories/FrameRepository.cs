using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

            // TODO: Commented code has caching issue... Check why?

            //List<Player> players = this.context.FramePlayer
            //    .Include(fp => fp.Player)
            //    .Where(fp => fp.FrameId == id)
            //    .Select(fp => fp.Player)
            //    .ToList();

            //foreach (Player player in players)
            //{
            //    // Include positions and scores from FramePlayer
            //    FramePlayer framePlayer = this.context.FramePlayer
            //        .Single(fp => fp.FrameId == id && fp.PlayerId == player.Id);
            //    player.Position = framePlayer.Position;
            //    player.Score = framePlayer.Score;

            //    // Include handicaps from PlayerTournament
            //    PlayerTournament playerTournament = this.context.PlayerTournament
            //        .SingleOrDefault(pt => pt.TournamentId == frame.TournamentId && pt.PlayerId == player.Id);
            //    if (playerTournament != null)
            //    {
            //        player.Handicap = playerTournament.Handicap;
            //    }
            //}

            //players = players.OrderBy(p => p.Position).ToList();

            //return players;

            List<FramePlayer> framePlayers = this.context.FramePlayer
                .Include(fp => fp.Player)
                .Where(fp => fp.FrameId == id)
                .ToList();

            List<Player> players = framePlayers.Select(fp => {
                Player player = new Player()
                {
                    Id = fp.Player.Id,
                    FirstName = fp.Player.FirstName,
                    LastName = fp.Player.LastName,
                    Position = fp.Position,
                    Score = fp.Score
                };

                // Include handicaps from PlayerTournament
                PlayerTournament playerTournament = this.context.PlayerTournament
                    .SingleOrDefault(pt => pt.TournamentId == frame.TournamentId && pt.PlayerId == player.Id);
                if (playerTournament != null)
                {
                    player.Handicap = playerTournament.Handicap;
                }

                return player;
            }).ToList();

            players = players.OrderBy(p => p.Position).ToList();

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
