using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class CourseReview
    {
        public Guid ReviewID { get; set; } = Guid.NewGuid();
        public Guid CourseID { get; set; }
        public Guid UserID { get; set; }
        public int Rating { get; set; } // 1..5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}