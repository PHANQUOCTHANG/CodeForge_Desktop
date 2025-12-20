using System;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class Profile
    {
        public Guid ProfileID { get; set; } = Guid.NewGuid();
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public string Country { get; set; }
        public int Points { get; set; } = 0;
        public int Level { get; set; } = 1;
        public bool IsDeleted { get; set; } = false;
    }
}