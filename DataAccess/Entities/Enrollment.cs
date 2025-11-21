using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Enrollment
    {
        public Guid EnrollmentID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public Guid CourseID { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "enrolled";
    }
}