using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SC.API.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid? PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }
    }
}
