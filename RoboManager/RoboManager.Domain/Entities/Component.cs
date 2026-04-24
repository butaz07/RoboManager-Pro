using RoboManager.Domain.Common;
using RoboManager.Domain.Enums;

namespace RoboManager.Domain.Entities
{
    public class Component : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public MaterialType Type { get; set; } = MaterialType.Hardware;
        public int Quantity { get; set; } = 1;
        public ComponentStatus Status { get; set; } = ComponentStatus.Available;

        public int? AssignedProjectId { get; set; }
        public virtual Project? AssignedProject { get; set; }
    }
}