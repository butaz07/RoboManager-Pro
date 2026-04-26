using System.Collections.Generic;
using System.Linq; 
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

        
        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var entities = await _unitOfWork.ProjectRepository.GetAllAsync();

            
            var activos = entities.Where(p => p.Activo).ToList();

            return _mapper.Map<IEnumerable<ProjectDto>>(activos);
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(int id)
        {
            var entity = await _unitOfWork.ProjectRepository.GetByIdAsync(id);

           
            if (entity == null || !entity.Activo) return null;

            return _mapper.Map<ProjectDto>(entity);
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectCreateDto dto)
        {
            var entity = _mapper.Map<Project>(dto);
            entity.Activo = true; 
            await _unitOfWork.ProjectRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ProjectDto>(entity);
        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectCreateDto dto)
        {
            var entity = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            if (entity == null || !entity.Activo) return false;

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