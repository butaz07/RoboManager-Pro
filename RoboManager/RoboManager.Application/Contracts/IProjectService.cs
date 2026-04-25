using System.Collections.Generic;
using System.Threading.Tasks;
using RoboManager.Application.DTOs;

namespace RoboManager.Application.Contracts
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(int id);
        Task<ProjectDto> CreateProjectAsync(ProjectCreateDto projectDto);
        Task<bool> UpdateProjectAsync(int id, ProjectCreateDto projectDto);
        Task<bool> DeleteProjectAsync(int id);
    }
}