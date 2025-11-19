using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // --- CRUD Implementation ---

        public User GetUserById(Guid id)
        {
            return _userRepository.GetById(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public bool CreateUser(User user)
        {
            // Business Logic: Có thể thêm kiểm tra Username/Email tồn tại ở đây
            if (_userRepository.IsUsernameOrEmailExist(user.Username, user.Email))
            {
                // Thông báo/log lỗi: Username hoặc Email đã tồn tại
                return false;
            }
            // Lưu ý: PasswordHash phải được thiết lập trước khi gọi Add (thường trong AuthService)

            return _userRepository.Add(user) > 0;
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.Update(user) > 0;
        }

        public bool SoftDeleteUser(Guid id)
        {
            return _userRepository.Delete(id) > 0;
        }
    }
}
