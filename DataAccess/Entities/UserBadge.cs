using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class UserBadge
    {
        public Guid UserID { get; set; }
        public Guid BadgeID { get; set; }
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
    }
}