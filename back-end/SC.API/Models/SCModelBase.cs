using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.Models
{
    public class SCModelBase
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        public DateTime CreatedOn { get; set; }
        public Guid CreatedUserId { get; set; }

        public DateTime ModifiedOn { get; set; }
        public Guid ModifiedUserId { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
