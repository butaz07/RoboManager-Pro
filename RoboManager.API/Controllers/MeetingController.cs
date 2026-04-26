using Microsoft.AspNetCore.Mvc;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;

namespace RoboManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingDto>>> GetAll()
        {
            return Ok(await _meetingService.GetAllMeetingsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingDto>> GetById(int id)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id);
            if (meeting == null) return NotFound();
            return Ok(meeting);
        }

        [HttpPost]
        public async Task<ActionResult<MeetingDto>> Create([FromBody] MeetingCreateDto dto)
        {
            var result = await _meetingService.CreateMeetingAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _meetingService.DeleteMeetingAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}