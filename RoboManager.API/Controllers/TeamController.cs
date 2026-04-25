using Microsoft.AspNetCore.Mvc;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoboManager.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAll()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams); 
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetById(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound(); 

            return Ok(team);
        }

        
        [HttpPost]
        public async Task<ActionResult<TeamDto>> Create([FromBody] TeamCreateDto teamDto)
        {
            var createdTeam = await _teamService.CreateTeamAsync(teamDto);

            
            return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TeamCreateDto teamDto)
        {
            var success = await _teamService.UpdateTeamAsync(id, teamDto);
            if (!success) return NotFound();

            return NoContent(); 
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _teamService.DeleteTeamAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}