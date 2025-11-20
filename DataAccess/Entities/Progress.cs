using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Progress
    {
        public Guid ProgressID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public Guid LessonID { get; set; }
        public string Status { get; set; } = "in_progress";
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}