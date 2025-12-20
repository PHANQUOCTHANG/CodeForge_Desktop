using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    // Match DB schema: Progress table contains only these columns
    public class Progress
    {
        public Guid ProgressID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public Guid LessonID { get; set; }
        public string Status { get; set; } = "in_progress"; // 'in_progress' | 'completed'
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}