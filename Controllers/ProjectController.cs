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

namespace ProjectManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Project>>>> GetProjects()
        {
            ServiceResponse<IEnumerable<Project>> response = new ServiceResponse<IEnumerable<Project>>();

            response.Data = await _context.Projects.ToListAsync();

            return response;
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Project>>> GetProject(int id)
        {
            ServiceResponse<Project> serviceResponse = new ServiceResponse<Project>();
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Project not found.";
                return NotFound(serviceResponse);
            }

            serviceResponse.Data = project;
            return serviceResponse;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(int id, Project project)
        {
            ServiceResponse<Project> serviceResponse = new ServiceResponse<Project>();

            if (id != project.ProjectId)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error in update project.";
                return BadRequest(serviceResponse);
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(e => e.ProjectId == id))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Project not found.";
                    return NotFound(serviceResponse);
                }
                else
                {
                    throw;
                }
            }

            serviceResponse.Data = project;

            return Ok(serviceResponse);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            ServiceResponse<List<Project>> serviceResponse = new ServiceResponse<List<Project>>();

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Project not found.";
                return NotFound(serviceResponse);
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Projects.ToListAsync();

            return Ok(serviceResponse);
        }
    }
}
