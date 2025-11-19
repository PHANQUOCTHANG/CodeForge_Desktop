using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Entities
{
    public class User
    {
        public Guid UserID { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "student";
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "active";
        public bool IsDeleted { get; set; } = false;

        //public User(string username, string email, string passwordHash, string role)
        //{
        //    Username = username;
        //    Email = email;
        //    PasswordHash = passwordHash;
        //    Role = role;
        //}
    }
}
