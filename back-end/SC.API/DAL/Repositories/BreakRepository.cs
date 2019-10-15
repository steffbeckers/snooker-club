using SC.API.Framework;
using SC.API.Models;

namespace SC.API.DAL.Repositories
{
    public class BreakRepository : Repository<Break>
    {
        private new readonly SCContext context;

        public BreakRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
