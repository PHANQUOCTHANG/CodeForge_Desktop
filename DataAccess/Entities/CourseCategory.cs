using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class CourseCategory
    {
        public Guid CategoryID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}