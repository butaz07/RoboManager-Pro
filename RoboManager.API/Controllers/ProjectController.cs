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

        // 1. Obtener todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll() => Ok(await _projectService.GetAllProjectsAsync());

        // 🔥 2. ESTE ES EL MÉTODO QUE FALTABA (Buscar por ID) 🔥
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            // Nota: Asumo que tu servicio tiene un método llamado GetProjectByIdAsync. 
            // Si se llama diferente (ej. GetByIdAsync), cámbialo aquí.
            var result = await _projectService.GetProjectByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        // 3. Crear
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] ProjectCreateDto dto)
        {
            var result = await _projectService.CreateProjectAsync(dto);
            return Ok(result);
        }

        // 4. Actualizar
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProjectCreateDto dto)
        {
            var updated = await _projectService.UpdateProjectAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        // 5. Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _projectService.DeleteProjectAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}