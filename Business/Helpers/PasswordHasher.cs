using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CodeForge_Desktop.Business.Helpers
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            // Tự động tạo salt và băm
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string password, string hash)
        {
            // Xác minh mật khẩu thường với mật khẩu đã băm
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
