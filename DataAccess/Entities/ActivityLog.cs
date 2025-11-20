using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class ActivityLog
    {
        public Guid LogID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}