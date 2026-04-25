using RoboManager.Domain.Common;
using RoboManager.Domain.Enums;
using System;

namespace RoboManager.Application.DTOs
{
    public class MeetingDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string LinkVirtual { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public MeetingStatus Estado { get; set; }
        public int ProjectId { get; set; }
    }

    public class MeetingCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string LinkVirtual { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public MeetingStatus Estado { get; set; }
        public int ProjectId { get; set; }
    }
}