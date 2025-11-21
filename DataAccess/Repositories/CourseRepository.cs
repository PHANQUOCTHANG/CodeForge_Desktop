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
        // safe helper to try multiple column name variants and return the first found value
        private object GetRowValue(DataRow row, params string[] candidates)
        {
            if (row == null || row.Table == null) return null;
            foreach (var name in candidates)
            {
                if (row.Table.Columns.Contains(name) && row[name] != DBNull.Value)
                    return row[name];
            }
            return null;
        }

        // Hàm helper để map DataRow sang Course object
        private Course MapRowToCourse(DataRow row)
        {
            // prefer CourseId or CourseID depending on DB schema
            var courseIdObj = GetRowValue(row, "CourseId", "CourseID");
            Guid courseId = courseIdObj != null ? (Guid)courseIdObj : Guid.Empty;

            return new Course
            {
                CourseId = courseId,
                Title = GetRowValue(row, "Title")?.ToString(),
                Description = GetRowValue(row, "Description")?.ToString(),
                Overview = GetRowValue(row, "Overview")?.ToString(),
                Level = GetRowValue(row, "Level")?.ToString(),
                Language = GetRowValue(row, "Language")?.ToString(),
                CategoryId = GetRowValue(row, "CategoryId") != null ? (Guid)GetRowValue(row, "CategoryId") : Guid.Empty,
                Price = GetRowValue(row, "Price") != null ? (decimal)GetRowValue(row, "Price") : 0m,
                Discount = GetRowValue(row, "Discount") != null ? (decimal)GetRowValue(row, "Discount") : 0m,
                Thumbnail = GetRowValue(row, "Thumbnail")?.ToString(),
                Status = GetRowValue(row, "Status")?.ToString(),
                CreatedAt = GetRowValue(row, "CreatedAt") != null ? (DateTime)GetRowValue(row, "CreatedAt") : DateTime.MinValue,
                IsDeleted = GetRowValue(row, "IsDeleted") != null ? Convert.ToBoolean(GetRowValue(row, "IsDeleted")) : false,
                LessonCount = GetRowValue(row, "LessonCount") != null ? Convert.ToInt32(GetRowValue(row, "LessonCount")) : 0,
                Duration = GetRowValue(row, "Duration") != null ? Convert.ToInt32(GetRowValue(row, "Duration")) : 0,
                Rating = GetRowValue(row, "Rating") != null ? Convert.ToDouble(GetRowValue(row, "Rating")) : 0.0,
                TotalStudents = GetRowValue(row, "TotalStudents") != null ? Convert.ToInt32(GetRowValue(row, "TotalStudents")) : 0,

                // derived fields (may be present in queries that join enrollments/progress)
                IsEnrolled = GetRowValue(row, "IsEnrolled") != null ? (Convert.ToInt32(GetRowValue(row, "IsEnrolled")) == 1) : false,
                ProgressPercentage = GetRowValue(row, "ProgressPercentage") != null ? Convert.ToInt32(GetRowValue(row, "ProgressPercentage")) : 0
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

        public async Task<List<Course>> GetListHasEnrollAsync(Guid userId)
        {
            return await Task.Run(() =>
            {
                // Query returns course columns plus:
                // - IsEnrolled (0/1) for the given user (Enrollments)
                // - ProgressPercentage computed as (completed lessons / total lessons) * 100
                // Completed lessons are counted from Progress where p.Status = 'completed'
                // and both Lessons and Modules are required to be not deleted (IsDeleted = 0).
                // This uses aggregated subqueries for totals and completed counts to avoid per-course queries.

                string sql = @"
IF EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Courses' AND COLUMN_NAME = 'IsDeleted'
)
BEGIN
    SELECT c.*,
           CASE WHEN e.EnrollmentID IS NULL THEN 0 ELSE 1 END AS IsEnrolled,
           CASE WHEN ISNULL(tl.TotalLessons,0) = 0 THEN 0
                ELSE CONVERT(INT, ROUND( ISNULL(cl.CompletedLessons,0) * 100.0 / tl.TotalLessons, 0))
           END AS ProgressPercentage
    FROM Courses c
    LEFT JOIN Enrollments e 
        ON e.CourseID = c.CourseID AND e.UserID = @UserId
    LEFT JOIN (
        -- total lessons per course where both lesson and its module are not deleted
        SELECT m.CourseID, COUNT(1) AS TotalLessons
        FROM Lessons l
        INNER JOIN Modules m ON l.ModuleID = m.ModuleID
        WHERE ISNULL(l.IsDeleted,0) = 0 AND ISNULL(m.IsDeleted,0) = 0
        GROUP BY m.CourseID
    ) tl ON tl.CourseID = c.CourseID
    LEFT JOIN (
        -- completed lessons per course for this user
        SELECT m.CourseID, COUNT(DISTINCT p.LessonID) AS CompletedLessons
        FROM Progress p
        INNER JOIN Lessons l ON p.LessonID = l.LessonID
        INNER JOIN Modules m ON l.ModuleID = m.ModuleID
        WHERE p.UserID = @UserId 
          AND ISNULL(p.Status,'') = 'completed'
          AND ISNULL(l.IsDeleted,0) = 0
          AND ISNULL(m.IsDeleted,0) = 0
        GROUP BY m.CourseID
    ) cl ON cl.CourseID = c.CourseID
    WHERE ISNULL(c.IsDeleted,0) = 0 AND ISNULL(c.Status,'') = 'active'
    ORDER BY c.CreatedAt DESC;
END
ELSE
BEGIN
    SELECT c.*,
           CASE WHEN e.EnrollmentID IS NULL THEN 0 ELSE 1 END AS IsEnrolled,
           CASE WHEN ISNULL(tl.TotalLessons,0) = 0 THEN 0
                ELSE CONVERT(INT, ROUND( ISNULL(cl.CompletedLessons,0) * 100.0 / tl.TotalLessons, 0))
           END AS ProgressPercentage
    FROM Courses c
    LEFT JOIN Enrollments e 
        ON e.CourseID = c.CourseID AND e.UserID = @UserId
    LEFT JOIN (
        SELECT m.CourseID, COUNT(1) AS TotalLessons
        FROM Lessons l
        INNER JOIN Modules m ON l.ModuleID = m.ModuleID
        WHERE ISNULL(l.IsDeleted,0) = 0 AND ISNULL(m.IsDeleted,0) = 0
        GROUP BY m.CourseID
    ) tl ON tl.CourseID = c.CourseID
    LEFT JOIN (
        SELECT m.CourseID, COUNT(DISTINCT p.LessonID) AS CompletedLessons
        FROM Progress p
        INNER JOIN Lessons l ON p.LessonID = l.LessonID
        INNER JOIN Modules m ON l.ModuleID = m.ModuleID
        WHERE p.UserID = @UserId 
          AND ISNULL(p.Status,'') = 'completed'
          AND ISNULL(l.IsDeleted,0) = 0
          AND ISNULL(m.IsDeleted,0) = 0
        GROUP BY m.CourseID
    ) cl ON cl.CourseID = c.CourseID
    WHERE ISNULL(c.Status,'') = 'active'
    ORDER BY c.CreatedAt DESC;
END
";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@UserId", userId));

                var list = new List<Course>();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(MapRowToCourse(row));
                    }
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
                string sql = "SELECT TOP 1 * FROM Courses WHERE CourseID = @Id AND IsDeleted = 0";
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
                    INSERT INTO Courses (CourseID, Title, Description, Overview, Level, Language, CreatedBy, CategoryID, Price, Discount, Thumbnail, Status, CreatedAt, IsDeleted, LessonCount, Duration, Rating, TotalStudents)
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
                    WHERE CourseID = @CourseId";

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
                // Soft Delete if column exists, otherwise hard delete
                string sql = @"
IF EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Courses' AND COLUMN_NAME = 'IsDeleted'
)
BEGIN
    UPDATE Courses SET IsDeleted = 1, UpdatedAt = @UpdatedAt WHERE CourseID = @Id;
END
ELSE
BEGIN
    DELETE FROM Courses WHERE CourseID = @Id;
END
";
                DbContext.Execute(sql,
                    new SqlParameter("@Id", id),
                    new SqlParameter("@UpdatedAt", DateTime.Now));
            });
        }

        public async Task<bool> ExistsByTitleAsync(string title, Guid? excludeId = null)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT COUNT(1) FROM Courses WHERE Title = @Title";
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Title", title)
                };

                if (excludeId.HasValue)
                {
                    sql += " AND CourseID != @ExcludeId";
                    parameters.Add(new SqlParameter("@ExcludeId", excludeId.Value));
                }

                DataTable dt = DbContext.Query(sql, parameters.ToArray());
                return dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0;
            });
        }

        public async Task<int> CountAsync()
        {
            return await Task.Run(() =>
            {
                // Count active rows; include safety if IsDeleted not present
                string sql = @"
IF EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Courses' AND COLUMN_NAME = 'IsDeleted'
)
BEGIN
    SELECT COUNT(*) FROM Courses WHERE IsDeleted = 0;
END
ELSE
BEGIN
    SELECT COUNT(*) FROM Courses;
END
";
                DataTable dt = DbContext.Query(sql);
                return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
            });
        }
    }
}
