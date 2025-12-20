using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Favorite
    {
        public Guid FavoriteID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public Guid? LessonID { get; set; }
        public Guid? ProblemID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}