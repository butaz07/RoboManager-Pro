namespace RoboManager.Application.DTOs
{
    // DTO para mostrar la información (Lectura)
    public class TeamDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    // DTO para recibir datos del Frontend (Creación/Actualización)
    public class TeamCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}