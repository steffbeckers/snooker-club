using Microsoft.EntityFrameworkCore;
using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class GroupPlayerRepository : Repository<GroupPlayer>
    {
        private new readonly SCContext context;

        public GroupPlayerRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public GroupPlayer GetByGroupAndPlayerId(Guid groupId, Guid playerId)
        {
            return this.context.GroupPlayer
                .Where(gp => gp.GroupId == groupId && gp.PlayerId == playerId)
                .SingleOrDefault();
        }
    }
}
