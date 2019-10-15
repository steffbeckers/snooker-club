using SC.API.Framework;
using SC.API.Models;

namespace SC.API.DAL.Repositories
{
    public class PlayerRepository : Repository<Player>
    {
        private new readonly SCContext context;

        public PlayerRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
