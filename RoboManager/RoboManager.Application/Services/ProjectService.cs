using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync() =>
            _mapper.Map<IEnumerable<ProjectDto>>(await _unitOfWork.ProjectRepository.GetAllAsync());

        public async Task<ProjectDto?> GetProjectByIdAsync(int id) =>
            _mapper.Map<ProjectDto>(await _unitOfWork.ProjectRepository.GetByIdAsync(id));

        public async Task<ProjectDto> CreateProjectAsync(ProjectCreateDto dto)
        {
            var entity = _mapper.Map<Project>(dto);
            await _unitOfWork.ProjectRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ProjectDto>(entity);
        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectCreateDto dto)
        {
            var entity = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _unitOfWork.ProjectRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var entity = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Activo = false;
            await _unitOfWork.ProjectRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
