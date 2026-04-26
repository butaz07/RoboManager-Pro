using AutoMapper;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.MappingProfiles
{
    public class AppProfiles : Profile
    {
        public AppProfiles()
        {
            
            CreateMap<Team, TeamDto>()
                .ForMember(d => d.Nombre, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Descripcion, o => o.MapFrom(s => s.Description));

            CreateMap<TeamCreateDto, Team>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Nombre))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Descripcion));

           
            CreateMap<Member, MemberDto>()
                .ForMember(d => d.Nombre, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.Apellido, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.Correo, o => o.MapFrom(s => s.Email)) 
                .ForMember(d => d.Rol, o => o.MapFrom(s => s.Role));

            CreateMap<MemberCreateDto, Member>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Nombre))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Apellido))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Correo))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Rol));

           
            CreateMap<Project, ProjectDto>()
                .ForMember(d => d.Nombre, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Descripcion, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.FechaInicio, o => o.MapFrom(s => s.StartDate))
                .ForMember(d => d.FechaFin, o => o.MapFrom(s => s.EndDate));

            CreateMap<ProjectCreateDto, Project>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Nombre))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Descripcion))
                .ForMember(d => d.StartDate, o => o.MapFrom(s => s.FechaInicio))
                .ForMember(d => d.EndDate, o => o.MapFrom(s => s.FechaFin));

           
            CreateMap<ProjectTask, ProjectTaskDto>().ReverseMap();
            CreateMap<ProjectTaskCreateDto, ProjectTask>();


            CreateMap<Component, ComponentDto>()
                .ForMember(d => d.Nombre, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Tipo, o => o.MapFrom(s => s.Type))
                .ForMember(d => d.Cantidad, o => o.MapFrom(s => s.Quantity))
                .ForMember(d => d.Estado, o => o.MapFrom(s => s.Status));

            CreateMap<ComponentCreateDto, Component>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Nombre))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Tipo))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Cantidad))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Estado));

            CreateMap<Meeting, MeetingDto>().ReverseMap();
            CreateMap<MeetingCreateDto, Meeting>();
        }
    }
}