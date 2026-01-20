using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class CourseReviewRepository : ICourseReviewRepository
    {
        // Helper SafeGet
        private T SafeGet<T>(DataRow row, string colName, T defaultValue = default)
        {
            if (!row.Table.Columns.Contains(colName)) return defaultValue;
            object value = row[colName];
            if (value == DBNull.Value || value == null) return defaultValue;
            try
            {
                if (typeof(T) == typeof(Guid)) return (T)(object)Guid.Parse(value.ToString());
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch { return defaultValue; }
        }

        private CourseReview MapToReview(DataRow row)
        {
            return new CourseReview
            {
                ReviewID = SafeGet<Guid>(row, "ReviewID"),
                CourseID = SafeGet<Guid>(row, "CourseID"),
                UserID = SafeGet<Guid>(row, "UserID"),
                Rating = SafeGet<int>(row, "Rating"),
                Comment = SafeGet<string>(row, "Comment"),
                CreatedAt = SafeGet<DateTime>(row, "CreatedAt"),
                UpdatedAt = SafeGet<DateTime>(row, "UpdatedAt"),

                // Map thêm User Name nếu query có join
                User = row.Table.Columns.Contains("Username") ? new User { Username = SafeGet<string>(row, "Username") } : null
            };
        }

        public async Task<List<CourseReview>> GetReviewsByCourseIdAsync(Guid courseId)
        {
            return await Task.Run(() =>
            {
                // Join với bảng Users để lấy tên người review
                string sql = @"
                    SELECT cr.*, u.Username 
                    FROM CourseReviews cr
                    LEFT JOIN Users u ON cr.UserID = u.UserID
                    WHERE cr.CourseID = @CId 
                    ORDER BY cr.CreatedAt DESC";

                DataTable dt = DbContext.Query(sql, new SqlParameter("@CId", courseId));
                var list = new List<CourseReview>();
                if (dt != null) foreach (DataRow r in dt.Rows) list.Add(MapToReview(r));
                return list;
            });
        }

        public async Task<CourseReview> GetReviewByUserAndCourseAsync(Guid userId, Guid courseId)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT TOP 1 * FROM CourseReviews WHERE UserID = @UId AND CourseID = @CId";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@UId", userId), new SqlParameter("@CId", courseId));
                if (dt != null && dt.Rows.Count > 0) return MapToReview(dt.Rows[0]);
                return null;
            });
        }

        public async Task<CourseReview> GetByIdAsync(Guid reviewId)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT TOP 1 * FROM CourseReviews WHERE ReviewID = @Id";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@Id", reviewId));
                if (dt != null && dt.Rows.Count > 0) return MapToReview(dt.Rows[0]);
                return null;
            });
        }

        public async Task<CourseReview> AddAsync(CourseReview r)
        {
            await Task.Run(() =>
            {
                if (r.ReviewID == Guid.Empty) r.ReviewID = Guid.NewGuid();
                if (r.CreatedAt == DateTime.MinValue) r.CreatedAt = DateTime.Now;
                r.UpdatedAt = DateTime.Now;

                string sql = @"INSERT INTO CourseReviews (ReviewID, CourseID, UserID, Rating, Comment, CreatedAt, UpdatedAt) 
                               VALUES (@Id, @CId, @UId, @Rating, @Comm, @Created, @Updated)";

                DbContext.Execute(sql,
                    new SqlParameter("@Id", r.ReviewID),
                    new SqlParameter("@CId", r.CourseID),
                    new SqlParameter("@UId", r.UserID),
                    new SqlParameter("@Rating", r.Rating),
                    new SqlParameter("@Comm", (object)r.Comment ?? DBNull.Value),
                    new SqlParameter("@Created", r.CreatedAt),
                    new SqlParameter("@Updated", r.UpdatedAt)
                );
            });
            return r;
        }

        public async Task<CourseReview> UpdateAsync(CourseReview r)
        {
            await Task.Run(() =>
            {
                r.UpdatedAt = DateTime.Now;
                string sql = "UPDATE CourseReviews SET Rating = @Rating, Comment = @Comm, UpdatedAt = @Updated WHERE ReviewID = @Id";

                DbContext.Execute(sql,
                    new SqlParameter("@Rating", r.Rating),
                    new SqlParameter("@Comm", (object)r.Comment ?? DBNull.Value),
                    new SqlParameter("@Updated", r.UpdatedAt),
                    new SqlParameter("@Id", r.ReviewID)
                );
            });
            return r;
        }

        public async Task<bool> DeleteAsync(Guid reviewId)
        {
            return await Task.Run(() =>
            {
                int rows = DbContext.Execute("DELETE FROM CourseReviews WHERE ReviewID = @Id", new SqlParameter("@Id", reviewId));
                return rows > 0;
            });
        }
    }
}