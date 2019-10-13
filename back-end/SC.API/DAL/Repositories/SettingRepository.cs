﻿using SC.API.Framework;
using SC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.DAL.Repositories
{
    public class SettingRepository : Repository<Setting>
    {
        private new readonly SCContext context;

        public SettingRepository(SCContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public Setting GetByKey(string key)
        {
            return this.context.Settings.SingleOrDefault(s => s.Key == key);
        }
    }
}
