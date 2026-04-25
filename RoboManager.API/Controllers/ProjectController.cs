using Microsoft.AspNetCore.Mvc;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;

namespace RoboManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService) => _projectService = projectService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll() => Ok(await _projectService.GetAllProjectsAsync());

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] ProjectCreateDto dto)
        {
            var result = await _projectService.CreateProjectAsync(dto);
            return Ok(result);
        }
    }
}