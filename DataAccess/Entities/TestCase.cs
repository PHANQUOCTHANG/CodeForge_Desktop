using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class TestCase
    {
        public Guid TestCaseID { get; set; } = Guid.NewGuid();
        public Guid ProblemID { get; set; }

        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
        public string Explain { get; set; }

        public bool IsHidden { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}