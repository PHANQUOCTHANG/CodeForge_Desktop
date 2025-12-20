using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeForge_Desktop.Business.Models;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface IAuthService
    {
        // Trả về User nếu thành công, ngược lại trả về null
        Response<User> Login(string username, string password);

        // Đăng ký người dùng mới
        // Nhận entity User (chưa có PasswordHash) và mật khẩu thường
        Response<User> Register(User user, string plainPassword);
    }
}
