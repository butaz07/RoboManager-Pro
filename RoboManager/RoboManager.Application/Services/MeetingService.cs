using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RoboManager.Application.Contracts;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MeetingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync() =>
            _mapper.Map<IEnumerable<MeetingDto>>(await _unitOfWork.MeetingRepository.GetAllAsync());

        public async Task<MeetingDto?> GetMeetingByIdAsync(int id) =>
            _mapper.Map<MeetingDto>(await _unitOfWork.MeetingRepository.GetByIdAsync(id));

        public async Task<MeetingDto> CreateMeetingAsync(MeetingCreateDto dto)
        {
            var entity = _mapper.Map<Meeting>(dto);
            await _unitOfWork.MeetingRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<MeetingDto>(entity);
        }

        public async Task<bool> UpdateMeetingAsync(int id, MeetingCreateDto dto)
        {
            var entity = await _unitOfWork.MeetingRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _unitOfWork.MeetingRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteMeetingAsync(int id)
        {
            var entity = await _unitOfWork.MeetingRepository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Activo = false;
            await _unitOfWork.MeetingRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}