using System;
using System.Threading.Tasks;
using RoboManager.Domain.Entities;

namespace RoboManager.Application.Contracts
{
    
    public interface IUnitOfWork : IDisposable
    {
        
        IGenericRepository<Team> TeamRepository { get; }

        

        Task<int> CompleteAsync(); 
    }
}