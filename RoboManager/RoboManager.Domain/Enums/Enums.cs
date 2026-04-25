namespace RoboManager.Domain.Enums
{
   

    public enum MemberRole
    {
        Estudiante, LiderDeProyecto, Profesor, Administrador
    }

    public enum MaterialType
    {
        Electronico, Mecanico, Estructural, Consumible, Herramienta, Otro
    }

    public enum ComponentStatus
    {
        Disponible, EnUso, Danado, EnReparacion, Extraviado
    }

    public enum ProjectStatus
    {
        Planificacion, EnDesarrollo, Completado, Pausado, Cancelado
    }

   

    public enum RoleType
    {
        Estudiante, LiderDeProyecto, Profesor, Administrador
    }

    public enum ComponentType
    {
        Microcontrolador, Sensor, Motor, Herramienta, Otro
    }

    public enum ProjectTaskStatus
    {
        Pendiente, EnProgreso, Completada, Cancelada
    }

    public enum MeetingStatus
    {
        Programada, EnCurso, Finalizada, Cancelada
    }
}