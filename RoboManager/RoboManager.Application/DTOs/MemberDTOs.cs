using RoboManager.Domain.Enums;


namespace RoboManager.Application.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public RoleType Rol { get; set; }
        public int TeamId { get; set; }
    }

    public class MemberCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public RoleType Rol { get; set; }
        public int TeamId { get; set; }
    }
}