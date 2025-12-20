
using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class CodingProblem
    {
        public Guid ProblemID { get; set; } = Guid.NewGuid();
        public Guid? LessonID { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Difficulty { get; set; } = "Dễ";
        public string Status { get; set; } = "NOT_STARTED";

        public string Description { get; set; }
        public string Tags { get; set; }
        public string FunctionName { get; set; }
        public string Parameters { get; set; }
        public string ReturnType { get; set; }
        public string Notes { get; set; }
        public string Constraints { get; set; }

        public int TimeLimit { get; set; } = 1000;
        public int MemoryLimit { get; set; } = 256;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public int SubmissionsCount { get; set; } = 0;
    }
}