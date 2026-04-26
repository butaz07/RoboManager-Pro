using RoboManager.Domain.Common;
using System;

namespace RoboManager.Domain.Entities
{
    public class Meeting : BaseEntity
    {
        public string Titulo { get; set; } = string.Empty;

       
        public string Proposito { get; set; } = string.Empty;

        public DateTime FechaHora { get; set; }

        
        public string Ubicacion { get; set; } = string.Empty;

        
        public string Estado { get; set; } = "Programada";

        
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}