using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
