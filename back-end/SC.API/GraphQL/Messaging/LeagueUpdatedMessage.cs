using System;
using System.Collections.Generic;

namespace SC.API.GraphQL.Messaging
{
    public class LeagueUpdatedMessage
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Season { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
