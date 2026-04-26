using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using AutoMapper;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComponentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       
        public async Task<IEnumerable<ComponentDto>> GetAllComponentsAsync()
        {
            var entities = await _unitOfWork.ComponentRepository.GetAllAsync();
            var activos = entities.Where(c => c.Activo).ToList();
            return _mapper.Map<IEnumerable<ComponentDto>>(activos);
        }

        
        public async Task<ComponentDto?> GetComponentByIdAsync(int id)
        {
            var entity = await _unitOfWork.ComponentRepository.GetByIdAsync(id);
            if (entity == null || !entity.Activo) return null;

            return _mapper.Map<ComponentDto>(entity);
        }

        public async Task<ComponentDto> CreateComponentAsync(ComponentCreateDto dto)
        {
            var entity = _mapper.Map<Component>(dto);
            entity.Activo = true; 
            await _unitOfWork.ComponentRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ComponentDto>(entity);
        }

        public async Task<bool> UpdateComponentAsync(int id, ComponentCreateDto dto)
        {
            var entity = await _unitOfWork.ComponentRepository.GetByIdAsync(id);
            
            if (entity == null || !entity.Activo) return false;

            _mapper.Map(dto, entity);
            await _unitOfWork.ComponentRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteComponentAsync(int id)
        {
            var entity = await _unitOfWork.ComponentRepository.GetByIdAsync(id);
            if (entity == null) return false;

            
            entity.Activo = false;
            await _unitOfWork.ComponentRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}