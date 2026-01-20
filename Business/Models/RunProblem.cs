using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Models
{
    public class RunProblem
    {
        public Guid UserId { get; set; }
        public Guid ProblemId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Language { get; set; }
        public string FunctionName { get; set; }
        public List<Guid> TestCases { get; set; }
    }
}
