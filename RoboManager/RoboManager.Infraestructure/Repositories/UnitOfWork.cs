using RoboManager.Application.Contracts;
using RoboManager.Domain.Entities;
using RoboManager.Infraestructura.Data;

using System;
using System.Threading.Tasks;

namespace RoboManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RoboManagerApplicationDbContext _context;
        private IGenericRepository<Team>? _teamRepository;
        private IGenericRepository<Member>? _memberRepository;
        private IGenericRepository<Project>? _projectRepository;
        private IGenericRepository<Component>? _componentRepository;
        private IGenericRepository<ProjectTask>? _taskRepository;
        private IGenericRepository<Meeting>? _meetingRepository;

        public UnitOfWork(RoboManagerApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Team> TeamRepository => _teamRepository ??= new GenericRepository<Team>(_context);
        public IGenericRepository<Member> MemberRepository => _memberRepository ??= new GenericRepository<Member>(_context);
        public IGenericRepository<Project> ProjectRepository => _projectRepository ??= new GenericRepository<Project>(_context);
        public IGenericRepository<Component> ComponentRepository => _componentRepository ??= new GenericRepository<Component>(_context);
        public IGenericRepository<ProjectTask> TaskRepository => _taskRepository ??= new GenericRepository<ProjectTask>(_context);
        public IGenericRepository<Meeting> MeetingRepository => _meetingRepository ??= new GenericRepository<Meeting>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}