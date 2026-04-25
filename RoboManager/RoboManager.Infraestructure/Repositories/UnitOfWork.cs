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

        public UnitOfWork(RoboManagerApplicationDbContext context)
        {
            _context = context;
        }

        
        public IGenericRepository<Team> TeamRepository =>
            _teamRepository ??= new GenericRepository<Team>(_context);

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