using System.Collections.Generic;
using System.Linq; // Necesario para el filtrado
using System.Threading.Tasks;
using AutoMapper;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // 🔥 FILTRO: Solo devolvemos miembros que estén marcados como Activo = true
        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            var entities = await _unitOfWork.MemberRepository.GetAllAsync();
            var activos = entities.Where(m => m.Activo).ToList();
            return _mapper.Map<IEnumerable<MemberDto>>(activos);
        }

        // 🔥 SEGURIDAD: Validamos que no se puedan obtener datos de un miembro "borrado"
        public async Task<MemberDto?> GetMemberByIdAsync(int id)
        {
            var entity = await _unitOfWork.MemberRepository.GetByIdAsync(id);
            if (entity == null || !entity.Activo) return null;

            return _mapper.Map<MemberDto>(entity);
        }

        public async Task<MemberDto> CreateMemberAsync(MemberCreateDto dto)
        {
            var entity = _mapper.Map<Member>(dto);
            entity.Activo = true; // Forzamos que el nuevo miembro entre con estado activo
            await _unitOfWork.MemberRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MemberDto>(entity);
        }

        public async Task<bool> UpdateMemberAsync(int id, MemberCreateDto dto)
        {
            var entity = await _unitOfWork.MemberRepository.GetByIdAsync(id);
            // 🔥 SEGURIDAD: Impedimos la edición si el miembro ha sido dado de baja
            if (entity == null || !entity.Activo) return false;

            _mapper.Map(dto, entity);
            await _unitOfWork.MemberRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var entity = await _unitOfWork.MemberRepository.GetByIdAsync(id);
            if (entity == null) return false;

            // BORRADO LÓGICO: Apagamos el switch de Activo
            entity.Activo = false;
            await _unitOfWork.MemberRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}