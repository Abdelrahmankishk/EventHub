using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    public class OrganizerProfile  // Fluent API in OnModelCreating
    {
        public int ProfileId { get; set; }
        public string Biography { get; set; } = default!;
        public string? WebsiteLink { get; set; }
        public string? LogoLink { get; set; }
        public int OrganizerId { get; set; }
        public Organizer ProfileOwner { get; set; } = default!;
    }
}
