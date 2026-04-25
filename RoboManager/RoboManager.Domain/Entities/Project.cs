using System.Collections.Generic;
using RoboManager.Domain.Common;
using RoboManager.Domain.Enums;

namespace RoboManager.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; } = ProjectStatus.Planificacion;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; } 


        public int? AssignedTeamId { get; set; }
        public virtual Team? AssignedTeam { get; set; }

        public virtual ICollection<Component> AssignedComponents { get; set; } = new List<Component>();
        public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
        public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}