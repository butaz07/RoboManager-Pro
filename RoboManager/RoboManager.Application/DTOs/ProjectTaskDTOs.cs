using RoboManager.Domain.Enums;
using System;

namespace RoboManager.Application.DTOs
{
    public class ProjectTaskDto
    {
        public int Id { get; set; }

        
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public ProjectTaskStatus Status { get; set; }

        public int ProjectId { get; set; }
        public int? AssignedMemberId { get; set; }
    }

    public class ProjectTaskCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public ProjectTaskStatus Status { get; set; }

        public int ProjectId { get; set; }
        public int? AssignedMemberId { get; set; }
    }
}