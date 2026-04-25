using AutoMapper;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
           
            CreateMap<Team, TeamDto>();

            
            CreateMap<TeamCreateDto, Team>();
        }
    }
}