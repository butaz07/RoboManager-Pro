using RoboManager.Application.DTOs;

namespace RoboManager.Application.Contracts
{
    public interface IMeetingService
    {
        Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync();
        Task<MeetingDto> GetMeetingByIdAsync(int id);
        Task<MeetingDto> CreateMeetingAsync(MeetingCreateDto dto);
        Task<bool> DeleteMeetingAsync(int id);
    }
}