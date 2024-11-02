using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public required string ProjectName { get; set; }

        public required string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Budget { get; set; }

        [Required]
        public int OwnerId { get; set; }
        public User? Owner { get; set; }

        public required string Status { get; set; } // Example statuses: "Not Started", "In Progress", "Completed"

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
