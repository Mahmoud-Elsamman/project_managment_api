using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Manager" },
                new Role { RoleId = 2, RoleName = "Employee" }
            );

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.ProjectsOwned) // Each user can own multiple projects
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.AssignedTo)
                .WithMany(u => u.TasksAssigned) // Each user can have multiple tasks assigned
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
