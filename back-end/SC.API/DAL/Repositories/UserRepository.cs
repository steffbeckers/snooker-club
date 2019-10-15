using SC.API.Framework;
using SC.API.Models;

namespace SC.API.DAL.Repositories
{
    public class UserRepository : Repository<User>
    {
        private new readonly SCContext context;

        public UserRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides
    }
}
