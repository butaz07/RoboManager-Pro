using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboManager.Domain.Common;
using RoboManager.Domain.Entities;

namespace RoboManager.Infraestructura.Data
{
    public class RoboManagerApplicationDbContext : DbContext
    {
        public RoboManagerApplicationDbContext(DbContextOptions<RoboManagerApplicationDbContext> options) 
            : base(options)
        {
        }

        
        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; } 
        public DbSet<Meeting> Meetings { get; set; }

        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.FechaCreacion = DateTime.UtcNow;
                        entry.Entity.Activo = true;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            
        }
    }
}