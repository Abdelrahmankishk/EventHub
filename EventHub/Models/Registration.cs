using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    public class Registration // Fluent API in OnModelCreating
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = default!;
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } = default!;
        public string? Note { get; set; }
        public DateTime RegistirationTime { get; set; }
    }
}
