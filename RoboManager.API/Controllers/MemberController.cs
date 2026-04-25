using Microsoft.AspNetCore.Mvc;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;

namespace RoboManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService) => _memberService = memberService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll() => Ok(await _memberService.GetAllMembersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetById(int id)
        {
            var result = await _memberService.GetMemberByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MemberDto>> Create([FromBody] MemberCreateDto dto)
        {
            var result = await _memberService.CreateMemberAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}