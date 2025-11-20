using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Submission
    {
        public Guid SubmissionID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProblemID { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public DateTime SubmitTime { get; set; }
        public int? ExecutionTime { get; set; } // ms
        public int? MemoryUsed { get; set; } // MB
        public int? QuantityTestPassed { get; set; }
        public int? QuantityTest { get; set; }
        public Guid? TestCaseIdFail { get; set; }
    }
}