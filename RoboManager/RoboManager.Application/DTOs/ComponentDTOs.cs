using RoboManager.Domain.Enums;

namespace RoboManager.Application.DTOs
{
    public class ComponentDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string NumeroSerie { get; set; } = string.Empty;
        public ComponentType Tipo { get; set; }
        public int ProjectId { get; set; }
    }

    public class ComponentCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string NumeroSerie { get; set; } = string.Empty;
        public ComponentType Tipo { get; set; }
        public int ProjectId { get; set; }
    }
}