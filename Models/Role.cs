using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public required string RoleName { get; set; } // Example roles: "Manager", "Employee"

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
