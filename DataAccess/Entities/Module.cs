using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Module
    {
        public Guid ModuleID { get; set; } = Guid.NewGuid();
        public Guid CourseID { get; set; }
        public string Title { get; set; }
        public int OrderIndex { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}