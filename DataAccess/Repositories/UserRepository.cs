using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Helper to convert a DataRow to a User object
        private User MapToUser(DataRow row)
        {
            return new User
            {
                // QUAN TRỌNG: Phải map ID từ DB vào, nếu không nó sẽ tự tạo Guid mới!
                UserID = (Guid)row["UserID"],

                Username = row["Username"].ToString(),
                Email = row["Email"].ToString(),
                PasswordHash = row["PasswordHash"].ToString(),
                Role = row["Role"].ToString(),

                // Map thêm các trường khác cho đầy đủ
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "Active",
                JoinDate = row["JoinDate"] != DBNull.Value ? (DateTime)row["JoinDate"] : DateTime.Now,

                IsDeleted = row["IsDeleted"] != DBNull.Value ? (bool)row["IsDeleted"] : false
            };
        }

        // --- CRUD Implementation ---

        public User GetById(Guid id)
        {
            string sql = "SELECT * FROM Users WHERE UserID = @id AND IsDeleted = 0";
            SqlParameter param = new SqlParameter("@id", id);
            DataTable dt = DbContext.Query(sql, param);

            return dt.Rows.Count > 0 ? MapToUser(dt.Rows[0]) : null;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            string sql = "SELECT * FROM Users WHERE IsDeleted = 0";
            DataTable dt = DbContext.Query(sql);

            foreach (DataRow row in dt.Rows)
            {
                users.Add(MapToUser(row));
            }
            return users;
        }

        public int Add(User user)
        {
            string sql = @"
                INSERT INTO Users (Username, Email, PasswordHash, Role)
                VALUES (@Username, @Email, @PasswordHash, @Role)";

            SqlParameter[] parameters = new SqlParameter[]
            {
               
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@Role", user.Role),
            };

            return DbContext.Execute(sql, parameters);
        }

        public int Update(User user)
        {
            string sql = @"
                UPDATE Users SET
                    Username = @Username,
                    Email = @Email,
                    PasswordHash = @PasswordHash,
                    Role = @Role,
                    Status = @Status
                WHERE UserID = @UserID AND IsDeleted = 0";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@Role", user.Role),
                new SqlParameter("@Status", user.Status)
            };

            return DbContext.Execute(sql, parameters);
        }

        public int Delete(Guid id) // Soft Delete
        {
            string sql = "UPDATE Users SET IsDeleted = 1, Status = 'deleted' WHERE UserID = @id";
            SqlParameter param = new SqlParameter("@id", id);

            return DbContext.Execute(sql, param);
        }

        // --- Auth Implementation ---

        public User GetByUsername(string username)
        {
            string sql = "SELECT * FROM Users WHERE Username = @username AND IsDeleted = 0";
            SqlParameter param = new SqlParameter("@username", username);
            DataTable dt = DbContext.Query(sql, param);

            return dt.Rows.Count > 0 ? MapToUser(dt.Rows[0]) : null;
        }

        public User GetByEmail(string email)
        {
            string sql = "SELECT * FROM Users WHERE Email = @email AND IsDeleted = 0";
            SqlParameter param = new SqlParameter("@email", email);
            DataTable dt = DbContext.Query(sql, param);

            return dt.Rows.Count > 0 ? MapToUser(dt.Rows[0]) : null;
        }

        public bool IsUsernameOrEmailExist(string username, string email)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE (Username = @username OR Email = @email) AND IsDeleted = 0";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@email", email)
            };

            DataTable dt = DbContext.Query(sql, parameters);

            int count = dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value ? (int)dt.Rows[0][0] : 0;

            return count > 0;
        }
    }
}
