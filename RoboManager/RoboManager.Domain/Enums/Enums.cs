namespace RoboManager.Domain.Enums
{
    public enum MemberRole
    {
        Leader, Programmer, Assembler, CAD_Designer, Documenter
    }

    public enum ProjectStatus
    {
        Planning, InDevelopment, Testing, Completed, Paused
    }

    public enum ComponentStatus
    {
        Available, InUse, OutOfStock, Damaged, Lost
    }

    public enum MaterialType
    {
        Hardware, Consumable, Tool
    }

    // RENOMBRADO para evitar conflicto con System.Threading.Tasks
    public enum ProjectTaskStatus
    {
        Pending, InProgress, Blocked, Completed
    }

    public enum MeetingStatus
    {
        Scheduled, Completed, Canceled, Rescheduled
    }
}