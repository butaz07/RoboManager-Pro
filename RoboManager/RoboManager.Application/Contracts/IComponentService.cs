using System.Collections.Generic;
using System.Threading.Tasks;
using RoboManager.Application.DTOs;

namespace RoboManager.Application.Contracts
{
    public interface IComponentService
    {
        Task<IEnumerable<ComponentDto>> GetAllComponentsAsync();
        Task<ComponentDto?> GetComponentByIdAsync(int id);
        Task<ComponentDto> CreateComponentAsync(ComponentCreateDto dto);
        Task<bool> UpdateComponentAsync(int id, ComponentCreateDto dto);
        Task<bool> DeleteComponentAsync(int id);
    }
}