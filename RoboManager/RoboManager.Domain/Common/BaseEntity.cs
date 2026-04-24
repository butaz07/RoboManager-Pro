using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboManager.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; } = true; // Útil para Soft Deletes (borrado lógico)
    }
}