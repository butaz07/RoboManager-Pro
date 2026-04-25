using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectTaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAllTasksAsync() =>
            _mapper.Map<IEnumerable<ProjectTaskDto>>(await _unitOfWork.TaskRepository.GetAllAsync());

        public async Task<ProjectTaskDto?> GetTaskByIdAsync(int id) =>
            _mapper.Map<ProjectTaskDto>(await _unitOfWork.TaskRepository.GetByIdAsync(id));

        public async Task<ProjectTaskDto> CreateTaskAsync(ProjectTaskCreateDto dto)
        {
            var entity = _mapper.Map<ProjectTask>(dto);
            await _unitOfWork.TaskRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ProjectTaskDto>(entity);
        }

        public async Task<bool> UpdateTaskAsync(int id, ProjectTaskCreateDto dto)
        {
            var entity = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _unitOfWork.TaskRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var entity = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Activo = false;
            await _unitOfWork.TaskRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}