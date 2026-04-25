using System;
using RoboManager.Domain.Common;
using RoboManager.Domain.Enums; 

namespace RoboManager.Domain.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Pendiente;

        public DateTime Deadline { get; set; }

        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public int? AssignedMemberId { get; set; }
        public virtual Member? AssignedMember { get; set; }
    }
}