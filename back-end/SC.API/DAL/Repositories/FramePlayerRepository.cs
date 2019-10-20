using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class FramePlayerRepository : Repository<FramePlayer>
    {
        private new readonly SCContext context;

        public FramePlayerRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public FramePlayer GetByFrameAndPlayerId(Guid frameId, Guid playerId)
        {
            return this.context.FramePlayer
                .Where(fp => fp.FrameId == frameId && fp.PlayerId == playerId)
                .SingleOrDefault();
        }
    }
}
