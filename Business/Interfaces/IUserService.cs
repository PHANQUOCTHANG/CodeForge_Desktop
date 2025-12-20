using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface IUserService
    {
        // CRUD Operations
        User GetUserById(Guid id);
        List<User> GetAllUsers();
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool SoftDeleteUser(Guid id);
        bool SoftDeleteListUsers(List<Guid> ids);
        User GetByUsername(string username);
        User GetByEmail(string email);
        public bool IsUsernameOrEmailExist(string username, string email);
    }
}
