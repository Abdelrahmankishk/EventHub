using EventHub.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    public class Badge // Data Annotation
    {
        public int BadgeId { get; set; }
        [Required]
        [MaxLength(30)]
        public string BadgeNum { get; set; } = default!;
        public DateTime IssueDate { get; set; }
        public BadgeTier Tier { get; set; }
        [ForeignKey(nameof(BadgeOwner))]
        public int AttendeeId { get; set; }
        public Attendee BadgeOwner { get; set; } = default!;
    }
}
