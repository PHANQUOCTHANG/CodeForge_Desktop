using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        // Hàm helper để map DataRow sang Course object
        private Course MapRowToCourse(DataRow row)
        {
            return new Course
            {
                CourseId = (Guid)row["CourseId"],       
                Title = row["Title"].ToString(),
                Description = row.Table.Columns.Contains("Description") && row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                Overview = row.Table.Columns.Contains("Overview") && row["Overview"] != DBNull.Value ? row["Overview"].ToString() : null,
                Level = row.Table.Columns.Contains("Level") && row["Level"] != DBNull.Value ? row["Level"].ToString() : null,
                Language = row.Table.Columns.Contains("Language") && row["Language"] != DBNull.Value ? row["Language"].ToString() : null,
                CategoryId = row.Table.Columns.Contains("CategoryId") && row["CategoryId"] != DBNull.Value ? (Guid)row["CategoryId"] : Guid.Empty,
                Price = row.Table.Columns.Contains("Price") && row["Price"] != DBNull.Value ? (decimal)row["Price"] : 0m,
                Discount = row.Table.Columns.Contains("Discount") && row["Discount"] != DBNull.Value ? (decimal)row["Discount"] : 0,
                Thumbnail = row.Table.Columns.Contains("Thumbnail") && row["Thumbnail"] != DBNull.Value ? row["Thumbnail"].ToString() : null,
                Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? row["Status"].ToString() : null,
                CreatedAt = row.Table.Columns.Contains("CreatedAt") && row["CreatedAt"] != DBNull.Value ? (DateTime)row["CreatedAt"] : DateTime.MinValue,
                IsDeleted = row.Table.Columns.Contains("IsDeleted") && row["IsDeleted"] != DBNull.Value ? (bool)row["IsDeleted"] : false,
                LessonCount = row.Table.Columns.Contains("LessonCount") && row["LessonCount"] != DBNull.Value ? (int)row["LessonCount"] : 0,
                Duration = row.Table.Columns.Contains("Duration") && row["Duration"] != DBNull.Value ? (int)row["Duration"] : 0,
                Rating = row.Table.Columns.Contains("Rating") && row["Rating"] != DBNull.Value ? Convert.ToDouble(row["Rating"]) : 0.0,
                TotalStudents = row.Table.Columns.Contains("TotalStudents") && row["TotalStudents"] != DBNull.Value ? Convert.ToInt32(row["TotalStudents"]) : 0
            };
        }

        public async Task<List<Course>> GetAllAsync()
        {
            // ADO.NET thường là đồng bộ (sync), ta bọc trong Task.Run để giả lập async cho UI mượt
            return await Task.Run(() =>
            {
                string sql = "SELECT * FROM Courses WHERE IsDeleted = 0 and Status = 'active' ORDER BY CreatedAt DESC";
                DataTable dt = DbContext.Query(sql);

                var list = new List<Course>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapRowToCourse(row));
                }
                return list;
            });
        }

        public async Task<List<Course>> SearchAsync(string keyword, string level = null)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT * FROM Courses WHERE IsDeleted = 0 and Status = 'active'";
                var parameters = new List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " AND Title LIKE @Keyword";
                    parameters.Add(new SqlParameter("@Keyword", "%" + keyword + "%"));
                }

                if (!string.IsNullOrWhiteSpace(level) && !level.Equals("Tất cả level", StringComparison.OrdinalIgnoreCase) && !level.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    sql += " AND Level = @Level";
                    parameters.Add(new SqlParameter("@Level", level));
                }

                sql += " ORDER BY CreatedAt DESC";

                DataTable dt = DbContext.Query(sql, parameters.ToArray());

                var list = new List<Course>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapRowToCourse(row));
                }
                return list;
            });
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT TOP 1 * FROM Courses WHERE CourseId = @Id AND IsDeleted = 0";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@Id", id));

                if (dt.Rows.Count > 0)
                {
                    return MapRowToCourse(dt.Rows[0]);
                }
                return null;
            });
        }

        public async Task AddAsync(Course course)
        {
            await Task.Run(() =>
            {
                // Đảm bảo ID
                if (course.CourseId == Guid.Empty) course.CourseId = Guid.NewGuid();
                course.CreatedAt = DateTime.Now;
                course.IsDeleted = false;

                string sql = @"
                    INSERT INTO Courses (CourseId, Title, Description, Overview, Level, Language, CreatedBy, CategoryID, Price, Discount, Thumbnail, Status, CreatedAt, IsDeleted, LessonCount, Duration, Rating, TotalStudents)
                    VALUES (@CourseId, @Title, @Description, @Overview, @Level, @Language, @CreatedBy, @CategoryId, @Price, @Discount, @Thumbnail, @Status, @CreatedAt, 0, @LessonCount, @Duration, @Rating, @TotalStudents)";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@CourseId", course.CourseId),
                    new SqlParameter("@Title", course.Title),
                    new SqlParameter("@Description", (object)course.Description ?? DBNull.Value),
                    new SqlParameter("@Overview", (object)course.Overview ?? DBNull.Value),
                    new SqlParameter("@Level", (object)course.Level ?? DBNull.Value),
                    new SqlParameter("@Language", (object)course.Language ?? DBNull.Value),
                    new SqlParameter("@CreatedBy", course.CategoryId == Guid.Empty ? (object)DBNull.Value : course.CategoryId), // placeholder if CreatedBy not provided
                    new SqlParameter("@CategoryId", course.CategoryId),
                    new SqlParameter("@Price", course.Price),
                    new SqlParameter("@Discount", course.Discount),
                    new SqlParameter("@Thumbnail", (object)course.Thumbnail ?? DBNull.Value),
                    new SqlParameter("@Status", (object)course.Status ?? DBNull.Value),
                    new SqlParameter("@CreatedAt", course.CreatedAt),
                    new SqlParameter("@LessonCount", course.LessonCount),
                    new SqlParameter("@Duration", course.Duration),
                    new SqlParameter("@Rating", course.Rating),
                    new SqlParameter("@TotalStudents", course.TotalStudents)
                };

                DbContext.Execute(sql, parameters);
            });
        }

        public async Task UpdateAsync(Course course)
        {
            await Task.Run(() =>
            {
                course.UpdatedAt = DateTime.Now;

                string sql = @"
                    UPDATE Courses 
                    SET Title = @Title, 
                        Description = @Description, 
                        Overview = @Overview,
                        Level = @Level, 
                        Language = @Language, 
                        CategoryId = @CategoryId, 
                        Price = @Price, 
                        Discount = @Discount, 
                        Thumbnail = @Thumbnail, 
                        Status = @Status, 
                        UpdatedAt = @UpdatedAt,
                        LessonCount = @LessonCount,
                        Duration = @Duration,
                        Rating = @Rating,
                        TotalStudents = @TotalStudents
                    WHERE CourseId = @CourseId";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Title", course.Title),
                    new SqlParameter("@Description", (object)course.Description ?? DBNull.Value),
                    new SqlParameter("@Overview", (object)course.Overview ?? DBNull.Value),
                    new SqlParameter("@Level", (object)course.Level ?? DBNull.Value),
                    new SqlParameter("@Language", (object)course.Language ?? DBNull.Value),
                    new SqlParameter("@CategoryId", course.CategoryId),
                    new SqlParameter("@Price", course.Price),
                    new SqlParameter("@Discount", course.Discount),
                    new SqlParameter("@Thumbnail", (object)course.Thumbnail ?? DBNull.Value),
                    new SqlParameter("@Status", (object)course.Status ?? DBNull.Value),
                    new SqlParameter("@UpdatedAt", course.UpdatedAt),
                    new SqlParameter("@CourseId", course.CourseId),
                    new SqlParameter("@LessonCount", course.LessonCount),
                    new SqlParameter("@Duration", course.Duration),
                    new SqlParameter("@Rating", course.Rating),
                    new SqlParameter("@TotalStudents", course.TotalStudents)
                };

                DbContext.Execute(sql, parameters);
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            await Task.Run(() =>
            {
                // Soft Delete
                string sql = "UPDATE Courses SET IsDeleted = 1, UpdatedAt = @UpdatedAt WHERE CourseId = @Id";
                DbContext.Execute(sql,
                    new SqlParameter("@Id", id),
                    new SqlParameter("@UpdatedAt", DateTime.Now));
            });
        }

        public async Task<bool> ExistsByTitleAsync(string title, Guid? excludeId = null)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT COUNT(1) FROM Courses WHERE Title = @Title AND IsDeleted = 0";
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Title", title)
                };

                if (excludeId.HasValue)
                {
                    sql += " AND CourseId != @ExcludeId";
                    parameters.Add(new SqlParameter("@ExcludeId", excludeId.Value));
                }

                DataTable dt = DbContext.Query(sql, parameters.ToArray());
                return (int)dt.Rows[0][0] > 0;
            });
        }

        public async Task<int> CountAsync()
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT COUNT(*) FROM Courses WHERE IsDeleted = 0";
                DataTable dt = DbContext.Query(sql);
                return (int)dt.Rows[0][0];
            });
        }
    }
}
