using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Models;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;


namespace CodeForge_Desktop.Business.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;

        public AuthService()
        {
            _userRepository = new UserRepository();
        }

        // Logic Đăng nhập
        public Response<User> Login(string username, string password)
        {
            User user;

            // 1. Tìm kiếm User bằng Username hoặc Email
            user = _userRepository.GetByUsername(username);

            if (user == null)
            {
                return new Response<User>
                {
                    Code = 0,
                    Message = "User không tồn tại" ,
                    Data = null
                };
            }

            // 2. Xác minh mật khẩu (dùng BCrypt)
            if (PasswordHasher.Verify(password, user.PasswordHash))
            {
                return new Response<User>
                {
                    Code = 1,
                    Message = "Đăng nhập thành công",
                    Data = user
                };
            }

            return new Response<User>
            {
                Code = 0,
                Message = "Tên đăng nhập hoặc mật khảu không đúng",
                Data = null
            };
        }

        // Logic Đăng ký
        public Response<User> Register(User user, string plainPassword)
        {
            // 1. Kiểm tra tồn tại
            if (_userRepository.IsUsernameOrEmailExist(user.Username, user.Email))
            {
                return new Response<User>
                {
                    Code = 0,
                    Message = "User đã tồn tại",
                    Data = null
                };
            }

            // 2. Băm mật khẩu và gán vào entity
            user.PasswordHash = PasswordHasher.Hash(plainPassword);

            // 3. Lưu vào database (sử dụng phương thức Add của Repository)
            if (_userRepository.Add(user) > 0)
            {
                return new Response<User>
                {
                    Code = 1,
                    Message = "Đăng kí thành công",
                    Data = user
                };
            }

            return new Response<User>
            {
                Code = 0,
                Message = "Lỗi khi đăng kí!",
                Data = null
            };
        }
    }
}
