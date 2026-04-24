using System.Collections.Generic;
using RoboManager.Domain.Common;

namespace RoboManager.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Relationships
        public virtual ICollection<Member> Members { get; set; } = new List<Member>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}