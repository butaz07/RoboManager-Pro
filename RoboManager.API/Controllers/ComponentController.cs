using Microsoft.AspNetCore.Mvc;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;

namespace RoboManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;

        public ComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetAll()
        {
            var components = await _componentService.GetAllComponentsAsync();
            return Ok(components);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDto>> GetById(int id)
        {
            var component = await _componentService.GetComponentByIdAsync(id);
            if (component == null) return NotFound();

            return Ok(component);
        }

        [HttpPost]
        public async Task<ActionResult<ComponentDto>> Create([FromBody] ComponentCreateDto dto)
        {
            var result = await _componentService.CreateComponentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ComponentCreateDto dto)
        {
            var updated = await _componentService.UpdateComponentAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _componentService.DeleteComponentAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}