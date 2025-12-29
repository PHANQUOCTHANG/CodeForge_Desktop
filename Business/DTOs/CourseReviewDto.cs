using System;

namespace CodeForge_Desktop.Business.DTOs
{
    public class CourseReviewDto
    {
        public Guid ReviewID { get; set; }
        public Guid CourseID { get; set; }
        public string User { get; set; } // Username
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}