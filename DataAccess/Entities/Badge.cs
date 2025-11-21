using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Badge
    {
        public Guid BadgeID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}