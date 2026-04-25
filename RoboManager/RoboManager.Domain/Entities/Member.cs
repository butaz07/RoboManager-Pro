using RoboManager.Domain.Common;
using RoboManager.Domain.Enums;

namespace RoboManager.Domain.Entities
{
    public class Member : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string EnrollmentId { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;
        public MemberRole Role { get; set; }

        
        public int? TeamId { get; set; }
        public virtual Team? Team { get; set; }
    }
}