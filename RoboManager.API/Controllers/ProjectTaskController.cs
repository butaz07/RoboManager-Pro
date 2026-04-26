using Microsoft.AspNetCore.Mvc;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;

namespace RoboManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService _taskService;
        public ProjectTaskController(IProjectTaskService taskService) => _taskService = taskService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTaskDto>>> GetAll() => Ok(await _taskService.GetAllTasksAsync());

        [HttpPost]
        public async Task<ActionResult<ProjectTaskDto>> Create([FromBody] ProjectTaskCreateDto dto)
        {
            var result = await _taskService.CreateTaskAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectTaskCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _taskService.UpdateTaskAsync(id, dto);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}