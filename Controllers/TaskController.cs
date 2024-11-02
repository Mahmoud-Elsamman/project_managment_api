using ASP_core_API.Models;
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
        public async Task<ActionResult> GetTasksByProject(int projectId)
        {
            ServiceResponse<IEnumerable<Task>> response = new ServiceResponse<IEnumerable<Task>>();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");

            IQueryable<Task> query = _context.Tasks
                .Where(t => t.ProjectId == projectId);


            if (User.IsInRole("Employee"))
            {
                query = query.Where(t => t.AssignedToId == userId);
            }

            var tasks = await query.ToListAsync();
            foreach (var t in tasks)
            {
                bool isOverdue = t.Status != "Completed" && t.EndDate < DateTime.UtcNow;
                t.IsOverdue = isOverdue;
            }

            response.Data = tasks;
            return Ok(response);
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
            ServiceResponse<Task> serviceResponse = new ServiceResponse<Task>();

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == id);

            if (task == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Task not found.";
                return NotFound(serviceResponse);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "Employee" && task.AssignedToId != userId)
            {

                return Forbid();
            }

            serviceResponse.Data = task;
            return Ok(serviceResponse);
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

            ServiceResponse<Task> serviceResponse = new ServiceResponse<Task>();

            if (id != task.TaskId)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in update task.";
                return BadRequest(serviceResponse);
            }

            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Task not found";
                return NotFound(serviceResponse);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");

            if (User.IsInRole("Employee") && existingTask.AssignedToId != userId)
            {
                return Forbid();
            }

            _context.Entry(existingTask).CurrentValues.SetValues(task);

            await _context.SaveChangesAsync();

            serviceResponse.Data = task;

            return Ok(serviceResponse);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            ServiceResponse<List<Project>> serviceResponse = new ServiceResponse<List<Project>>();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Task not found.";
                return NotFound(serviceResponse);
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(serviceResponse);
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