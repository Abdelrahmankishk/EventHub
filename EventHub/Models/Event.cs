using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    public class Event // Separate Cofiguration Class
    {
        public int EventId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int MaxAttendees { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? ParentEventId { get; set; }
        public Event? ParenEvent { get; set; }
        public ICollection<Event> Sessions { get; set; } = new List<Event>();
        public ICollection<Registration> Registirations { get; set; }
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = default!;
    }
}
