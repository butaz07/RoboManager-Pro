using System.Collections.Generic;
using System.Threading.Tasks;
using RoboManager.Application.DTOs;

namespace RoboManager.Application.Contracts
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();
        Task<MemberDto?> GetMemberByIdAsync(int id);
        Task<MemberDto> CreateMemberAsync(MemberCreateDto memberDto);
        Task<bool> UpdateMemberAsync(int id, MemberCreateDto memberDto);
        Task<bool> DeleteMemberAsync(int id);
    }
}