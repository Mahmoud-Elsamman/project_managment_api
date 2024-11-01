using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Task = ProjectManagementApp.Models.Task;

namespace ProjectManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasksByProject(int projectId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");

            IQueryable<Task> query = _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.AssignedTo);

            if (User.IsInRole("Employee"))
            {
                query = query.Where(t => t.AssignedToId == userId);
            }

            var tasks = await query.ToListAsync();
            return tasks;
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "Employee")
            {
                return await _context.Tasks
                    .Where(t => t.AssignedToId == userId)
                    .Include(t => t.AssignedTo)
                    .ToListAsync();
            }

            return await _context.Tasks.Include(t => t.AssignedTo).ToListAsync();
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.Include(t => t.AssignedTo).FirstOrDefaultAsync(t => t.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "Employee" && task.AssignedToId != userId)
            {
                return Forbid();
            }

            return task;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<Task>> CreateTask(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Task task)
        {
            if (id != task.TaskId)
            {
                return BadRequest();
            }

            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");

            if (User.IsInRole("Employee") && existingTask.AssignedToId != userId)
            {
                return Forbid();
            }

            _context.Entry(existingTask).CurrentValues.SetValues(task);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<Task>>> GetOverdueTasks()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");

            IQueryable<Task> query = _context.Tasks
                .Where(t => t.EndDate < DateTime.Now && t.Status != "Completed");

            if (User.IsInRole("Employee"))
            {
                query = query.Where(t => t.AssignedToId == userId);
            }

            var overdueTasks = await query.ToListAsync();
            return overdueTasks;
        }
    }
}
