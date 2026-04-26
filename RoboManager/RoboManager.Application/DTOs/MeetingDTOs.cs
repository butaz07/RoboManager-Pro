using System;

namespace RoboManager.Application.DTOs
{
    public class MeetingDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Proposito { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int ProjectId { get; set; }
    }

    public class MeetingCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Proposito { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string Estado { get; set; } = "Programada";
        public int ProjectId { get; set; }
    }
}