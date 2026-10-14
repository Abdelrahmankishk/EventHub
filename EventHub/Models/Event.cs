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
    }
}
