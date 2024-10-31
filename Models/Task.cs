using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public required string TaskName { get; set; }

        public required string Description { get; set; }

        [Required]
        public required string AssignedTo { get; set; } // Could link to a User in a more advanced setup

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public required string Priority { get; set; } // Example priorities: "Low", "Medium", "High"

        public required string Status { get; set; } // Example statuses: "Not Started", "In Progress", "Completed"

        [Required]
        public int ProjectId { get; set; }

        public required Project Project { get; set; }
    }
}
