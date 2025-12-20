using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        // CRUD Operations
        User GetById(Guid id);
        List<User> GetAll();
        int Add(User user);
        int Update(User user);
        int Delete(Guid id); // Soft delete

        // Auth Operations
        User GetByUsername(string username);
        User GetByEmail(string email);
        bool IsUsernameOrEmailExist(string username, string email);
    }
}
