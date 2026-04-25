using System.Collections.Generic;
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

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync() => _mapper.Map<IEnumerable<MemberDto>>(await _unitOfWork.MemberRepository.GetAllAsync());

        public async Task<MemberDto?> GetMemberByIdAsync(int id) => _mapper.Map<MemberDto>(await _unitOfWork.MemberRepository.GetByIdAsync(id));

        public async Task<MemberDto> CreateMemberAsync(MemberCreateDto dto)
        {
            var entity = _mapper.Map<Member>(dto);
            await _unitOfWork.MemberRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MemberDto>(entity);
        }

        public async Task<bool> UpdateMemberAsync(int id, MemberCreateDto dto)
        {
            var entity = await _unitOfWork.MemberRepository.GetByIdAsync(id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            await _unitOfWork.MemberRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var entity = await _unitOfWork.MemberRepository.GetByIdAsync(id);
            if (entity == null) return false;
            entity.Activo = false;
            await _unitOfWork.MemberRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}