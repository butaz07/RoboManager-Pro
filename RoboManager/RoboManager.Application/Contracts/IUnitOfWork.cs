using System;
using System.Threading.Tasks;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Team> TeamRepository { get; }
        
        IGenericRepository<Member> MemberRepository { get; }
        IGenericRepository<Project> ProjectRepository { get; }
        IGenericRepository<Component> ComponentRepository { get; }
        IGenericRepository<ProjectTask> TaskRepository { get; }
        IGenericRepository<Meeting> MeetingRepository { get; }

        Task<int> CompleteAsync();
    }
}