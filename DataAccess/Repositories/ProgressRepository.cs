using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        public async Task<bool> MarkCompletedAsync(Guid userId, Guid lessonId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Logic UPSERT: Kiểm tra tồn tại -> Insert hoặc Update
                    string checkSql = "SELECT COUNT(1) FROM Progress WHERE UserID = @U AND LessonID = @L";
                    int count = Convert.ToInt32(DbContext.ExecuteScalar(checkSql, new SqlParameter("@U", userId), new SqlParameter("@L", lessonId)));

                    if (count == 0)
                    {
                        // Insert mới
                        string insertSql = @"INSERT INTO Progress (ProgressID, UserID, LessonID, Status, UpdatedAt) 
                                             VALUES (@Id, @U, @L, 'completed', GETDATE())";
                        DbContext.Execute(insertSql,
                            new SqlParameter("@Id", Guid.NewGuid()),
                            new SqlParameter("@U", userId),
                            new SqlParameter("@L", lessonId));
                    }
                    else
                    {
                        // Update trạng thái (nếu cần thiết, ví dụ muốn đổi ngày cập nhật)
                        string updateSql = "UPDATE Progress SET Status = 'completed', UpdatedAt = GETDATE() WHERE UserID = @U AND LessonID = @L";
                        DbContext.Execute(updateSql, new SqlParameter("@U", userId), new SqlParameter("@L", lessonId));
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    // Log error here
                    Console.WriteLine("Progress Error: " + ex.Message);
                    return false;
                }
            });
        }

        public async Task<List<Guid>> GetCompletedLessonsAsync(Guid userId, Guid courseId)
        {
            return await Task.Run(() =>
            {
                // Join 3 bảng để lọc ra các bài học thuộc khóa học này mà user đã hoàn thành
                string sql = @"
                    SELECT p.LessonID 
                    FROM Progress p
                    JOIN Lessons l ON p.LessonID = l.LessonID
                    JOIN Modules m ON l.ModuleID = m.ModuleID
                    WHERE p.UserID = @U 
                      AND m.CourseID = @C 
                      AND ISNULL(p.Status, '') = 'completed'";

                DataTable dt = DbContext.Query(sql, new SqlParameter("@U", userId), new SqlParameter("@C", courseId));
                var list = new List<Guid>();

                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["LessonID"] != DBNull.Value)
                            list.Add((Guid)r["LessonID"]);
                    }
                }
                return list;
            });
        }

        public async Task<double> GetProgressPercentageAsync(Guid userId, Guid courseId)
        {
            return await Task.Run(() =>
            {
                // 1. Đếm tổng số bài học trong khóa
                string sqlTotal = @"
                    SELECT COUNT(*) 
                    FROM Lessons l
                    JOIN Modules m ON l.ModuleID = m.ModuleID
                    WHERE m.CourseID = @C AND ISNULL(l.IsDeleted, 0) = 0";

                int totalLessons = Convert.ToInt32(DbContext.ExecuteScalar(sqlTotal, new SqlParameter("@C", courseId)));

                if (totalLessons == 0) return 0.0;

                // 2. Đếm số bài đã hoàn thành
                string sqlCompleted = @"
                    SELECT COUNT(DISTINCT p.LessonID)
                    FROM Progress p
                    JOIN Lessons l ON p.LessonID = l.LessonID
                    JOIN Modules m ON l.ModuleID = m.ModuleID
                    WHERE p.UserID = @U 
                      AND m.CourseID = @C 
                      AND ISNULL(p.Status, '') = 'completed'";

                int completedLessons = Convert.ToInt32(DbContext.Execute(sqlCompleted, new SqlParameter("@U", userId), new SqlParameter("@C", courseId)));

                // 3. Tính phần trăm
                return Math.Round(((double)completedLessons / totalLessons) * 100, 1);
            });
        }
    }
}