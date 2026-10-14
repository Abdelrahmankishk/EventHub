using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    public class Attendee   // Separate Configuration Class
    {
        public int AttendeeId { get; set; }
        public string FullName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public Address HomeAddress { get; set; } = new();
        public ICollection<Registration> Registirations { get; set; }
        public Badge? EarnedBadge { get; set; }
    }
}
