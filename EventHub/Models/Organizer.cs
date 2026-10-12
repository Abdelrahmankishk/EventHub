using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models
{
    public class Organizer // Data Annotations
    {
        public int OrganizerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(100)]
        public string? CompanyName { get; set; }
        public bool IsVerified { get; set; }
    }
}
