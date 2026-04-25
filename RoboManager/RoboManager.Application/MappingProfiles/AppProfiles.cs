using AutoMapper;
using RoboManager.Application.DTOs;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.MappingProfiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile() { CreateMap<Member, MemberDto>(); CreateMap<MemberCreateDto, Member>(); }
    }

    public class ProjectProfile : Profile
    {
        public ProjectProfile() { CreateMap<Project, ProjectDto>(); CreateMap<ProjectCreateDto, Project>(); }
    }

    public class ComponentProfile : Profile
    {
        public ComponentProfile() { CreateMap<Component, ComponentDto>(); CreateMap<ComponentCreateDto, Component>(); }
    }

    public class ProjectTaskProfile : Profile
    {
        public ProjectTaskProfile() { CreateMap<ProjectTask, ProjectTaskDto>(); CreateMap<ProjectTaskCreateDto, ProjectTask>(); }
    }

    public class MeetingProfile : Profile
    {
        public MeetingProfile() { CreateMap<Meeting, MeetingDto>(); CreateMap<MeetingCreateDto, Meeting>(); }
    }
}