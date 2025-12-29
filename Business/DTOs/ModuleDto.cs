using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.DTOs
{
        public class ModuleDto
        {
            public Guid ModuleID { get; set; }
            public Guid CourseID { get; set; }
            public string Title { get; set; } = string.Empty;
            public int OrderIndex { get; set; }
            public bool IsDeleted { get; set; } = false;

            /// <summary>
            /// List of lessons belonging to this module.
            /// </summary>
            public List<LessonDto> Lessons { get; set; } = new List<LessonDto>();
        }
}
