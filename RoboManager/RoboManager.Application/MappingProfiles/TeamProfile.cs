using AutoMapper;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            // De Entidad a DTO (Para cuando leemos de la base de datos)
            CreateMap<Team, TeamDto>();

            // De DTO a Entidad (Para cuando creamos o actualizamos desde el Frontend)
            CreateMap<TeamCreateDto, Team>();
        }
    }
}