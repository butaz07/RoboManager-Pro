using System.Collections.Generic;
using System.Threading.Tasks;
using RoboManager.Application.DTOs;

namespace RoboManager.Application.Contracts
{
    public interface IProjectTaskService
    {
        Task<IEnumerable<ProjectTaskDto>> GetAllTasksAsync();
        Task<ProjectTaskDto?> GetTaskByIdAsync(int id);
        Task<ProjectTaskDto> CreateTaskAsync(ProjectTaskCreateDto dto);
        Task<bool> UpdateTaskAsync(int id, ProjectTaskCreateDto dto);
        Task<bool> DeleteTaskAsync(int id);
    }
}