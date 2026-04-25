using System.Collections.Generic;
using System.Threading.Tasks;
using RoboManager.Application.DTOs;

namespace RoboManager.Application.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto?> GetTeamByIdAsync(int id);
        Task<TeamDto> CreateTeamAsync(TeamCreateDto teamDto);
        Task<bool> UpdateTeamAsync(int id, TeamCreateDto teamDto);
        Task<bool> DeleteTeamAsync(int id);
    }
}