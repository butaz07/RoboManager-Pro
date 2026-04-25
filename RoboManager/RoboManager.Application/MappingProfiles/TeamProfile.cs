using AutoMapper;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description));

            CreateMap<TeamCreateDto, Team>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion));
        }
    }
}