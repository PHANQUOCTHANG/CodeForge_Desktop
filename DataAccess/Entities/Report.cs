using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Report
    {
        public Guid ReportID { get; set; } = Guid.NewGuid();
        public Guid ReporterID { get; set; }
        public string TargetType { get; set; } // user, comment, thread, lesson, problem
        public Guid TargetID { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } = "pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}