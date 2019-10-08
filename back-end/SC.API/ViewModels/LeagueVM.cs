using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.ViewModels
{
    public class LeagueVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Season { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
