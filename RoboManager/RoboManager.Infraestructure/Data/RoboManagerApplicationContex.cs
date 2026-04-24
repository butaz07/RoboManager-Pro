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
        // El constructor que recibe las opciones de conexión (vital para inyectarlo en la API)
        public RoboManagerApplicationDbContext(DbContextOptions<RoboManagerApplicationDbContext> options) : base(options)
        {
        }

        // Tus tablas en la base de datos
        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        // 🔥 TOQUE PROFESIONAL: Interceptar el guardado para autocompletar la auditoría
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
                    case EntityState.Modified:
                        entry.Entity.FechaModificacion = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        // Aquí configuramos las llaves foráneas y reglas estrictas si es necesario (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ejemplo rápido: Forzar que el nombre del proyecto sea único o requerido, 
            // aunque por ahora Entity Framework entenderá las relaciones automáticamente
            // gracias a cómo estructuramos el Dominio.
        }
    }
}