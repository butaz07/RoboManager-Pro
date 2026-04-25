using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _unitOfWork.TeamRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

        public async Task<TeamDto?> GetTeamByIdAsync(int id)
        {
            var team = await _unitOfWork.TeamRepository.GetByIdAsync(id);
            if (team == null) return null;
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<TeamDto> CreateTeamAsync(TeamCreateDto teamDto)
        {
            var teamEntity = _mapper.Map<Team>(teamDto);

            await _unitOfWork.TeamRepository.AddAsync(teamEntity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TeamDto>(teamEntity);
        }

        public async Task<bool> UpdateTeamAsync(int id, TeamCreateDto teamDto)
        {
            var existingTeam = await _unitOfWork.TeamRepository.GetByIdAsync(id);
            if (existingTeam == null) return false;

            _mapper.Map(teamDto, existingTeam);

            await _unitOfWork.TeamRepository.UpdateAsync(existingTeam);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await _unitOfWork.TeamRepository.GetByIdAsync(id);
            if (team == null) return false;

            team.Activo = false;
            await _unitOfWork.TeamRepository.UpdateAsync(team);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}