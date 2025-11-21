using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class LanguageEntity
    {
        public Guid LanguageID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Version { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}