using Microsoft.AspNetCore.Mvc;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;

namespace RoboManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IProjectTaskService _taskService;
        public TaskController(IProjectTaskService taskService) => _taskService = taskService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTaskDto>>> GetAll() => Ok(await _taskService.GetAllTasksAsync());

        [HttpPost]
        public async Task<ActionResult<ProjectTaskDto>> Create([FromBody] ProjectTaskCreateDto dto)
        {
            var result = await _taskService.CreateTaskAsync(dto);
            return Ok(result);
        }
    }
}