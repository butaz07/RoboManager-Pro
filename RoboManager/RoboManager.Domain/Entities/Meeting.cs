using System;
using RoboManager.Domain.Common;
using RoboManager.Domain.Enums;

namespace RoboManager.Domain.Entities
{
    public class Meeting : BaseEntity
    {
        public string Purpose { get; set; } = string.Empty;
        public DateTime ScheduledAt { get; set; }
        public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;
        public string Notes { get; set; } = string.Empty;

        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}