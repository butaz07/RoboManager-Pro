using RoboManager.Domain.Common;
using RoboManager.Domain.Enums;

namespace RoboManager.Domain.Entities
{
    public class Member : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string EnrollmentId { get; set; } = string.Empty; // Matrícula
        public string Email { get; set; } = string.Empty;
        public MemberRole Role { get; set; }

        // Relationship: A member belongs to a Team
        public int? TeamId { get; set; }
        public virtual Team? Team { get; set; }
    }
}