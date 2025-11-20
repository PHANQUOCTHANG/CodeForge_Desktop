using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Notification
    {
        public Guid NotificationID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}