using RoboManager.Domain.Common;
using RoboManager.Domain.Enums;
using System;

namespace RoboManager.Application.DTOs
{
    public class ProjectTaskDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaLimite { get; set; }
        public ProjectTaskStatus Estado { get; set; }
        public int ProjectId { get; set; }
        public int? AsignadoAId { get; set; }
    }

    public class ProjectTaskCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaLimite { get; set; }
        public ProjectTaskStatus Estado { get; set; }
        public int ProjectId { get; set; }
        public int? AsignadoAId { get; set; }
    }
}