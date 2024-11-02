using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required byte[] PasswordHash { get; set; }

        [Required]
        public required byte[] PasswordSalt { get; set; }

        [Required]
        public int RoleId { get; set; }

        public Role? Role { get; set; }

        public ICollection<Project> ProjectsOwned { get; set; } = new List<Project>(); // Projects owned by the user
        public ICollection<Task> TasksAssigned { get; set; } = new List<Task>(); // Tasks assigned to the user
    }
}
