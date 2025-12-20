using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        private Progress MapToProgress(DataRow row)
        {
            return new Progress
            {
                ProgressID = row.Table.Columns.Contains("ProgressID") && row["ProgressID"] != DBNull.Value ? (Guid)row["ProgressID"] : Guid.Empty,
                UserID = row.Table.Columns.Contains("UserID") && row["UserID"] != DBNull.Value ? (Guid)row["UserID"] : Guid.Empty,
                LessonID = row.Table.Columns.Contains("LessonID") && row["LessonID"] != DBNull.Value ? (Guid)row["LessonID"] : Guid.Empty,
                Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? row["Status"].ToString() : "in_progress",
                UpdatedAt = row.Table.Columns.Contains("UpdatedAt") && row["UpdatedAt"] != DBNull.Value ? (DateTime)row["UpdatedAt"] : DateTime.MinValue
            };
        }

        public Progress GetById(Guid id)
        {
            string sql = "SELECT TOP 1 ProgressID, UserID, LessonID, Status, UpdatedAt FROM Progress WHERE ProgressID = @id";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@id", id));
            return dt.Rows.Count > 0 ? MapToProgress(dt.Rows[0]) : null;
        }

        public List<Progress> GetByUserAndCourse(Guid userId, Guid courseId)
        {
            // join Lessons -> Modules to filter by course
            string sql = @"
                SELECT p.ProgressID, p.UserID, p.LessonID, p.Status, p.UpdatedAt
                FROM Progress p
                INNER JOIN Lessons l ON p.LessonID = l.LessonID
                INNER JOIN Modules m ON l.ModuleID = m.ModuleID
                WHERE p.UserID = @userId AND m.CourseID = @courseId";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@userId", userId), new SqlParameter("@courseId", courseId));
            var list = new List<Progress>();
            foreach (DataRow r in dt.Rows) list.Add(MapToProgress(r));
            return list;
        }

        public Progress GetByUserLessonAndCourse(Guid userId, Guid lessonId, Guid courseId)
        {
            // ensure the lesson belongs to the course by joining Modules
            string sql = @"
                SELECT TOP 1 p.ProgressID, p.UserID, p.LessonID, p.Status, p.UpdatedAt
                FROM Progress p
                INNER JOIN Lessons l ON p.LessonID = l.LessonID
                INNER JOIN Modules m ON l.ModuleID = m.ModuleID
                WHERE p.UserID = @userId AND p.LessonID = @lessonId AND m.CourseID = @courseId";
            DataTable dt = DbContext.Query(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@lessonId", lessonId),
                new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? MapToProgress(dt.Rows[0]) : null;
        }

        public int Add(Progress progress)
        {
            if (progress.ProgressID == Guid.Empty) progress.ProgressID = Guid.NewGuid();
            // Insert only columns present in DB schema
            string sql = @"
                INSERT INTO Progress (ProgressID, UserID, LessonID, Status, UpdatedAt)
                VALUES (@ProgressID, @UserID, @LessonID, @Status, @UpdatedAt)";
            return DbContext.Execute(sql,
                new SqlParameter("@ProgressID", progress.ProgressID),
                new SqlParameter("@UserID", progress.UserID),
                new SqlParameter("@LessonID", progress.LessonID),
                new SqlParameter("@Status", (object)progress.Status ?? "in_progress"),
                new SqlParameter("@UpdatedAt", progress.UpdatedAt == DateTime.MinValue ? DateTime.UtcNow : progress.UpdatedAt));
        }

        public int Update(Progress progress)
        {
            progress.UpdatedAt = DateTime.UtcNow;
            string sql = @"
                UPDATE Progress
                SET Status = @Status, UpdatedAt = @UpdatedAt
                WHERE ProgressID = @ProgressID";
            return DbContext.Execute(sql,
                new SqlParameter("@Status", (object)progress.Status ?? "in_progress"),
                new SqlParameter("@UpdatedAt", progress.UpdatedAt),
                new SqlParameter("@ProgressID", progress.ProgressID));
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Progress WHERE ProgressID = @id";
            return DbContext.Execute(sql, new SqlParameter("@id", id));
        }

        public int GetCompletedLessonCount(Guid userId, Guid courseId)
        {
            string sql = @"
                SELECT COUNT(DISTINCT p.LessonID)
                FROM Progress p
                INNER JOIN Lessons l ON p.LessonID = l.LessonID
                INNER JOIN Modules m ON l.ModuleID = m.ModuleID
                WHERE p.UserID = @userId AND m.CourseID = @courseId AND p.Status = 'completed'";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@userId", userId), new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        public int GetTotalLessonCount(Guid courseId)
        {
            string sql = @"
                SELECT COUNT(1)
                FROM Lessons l
                INNER JOIN Modules m ON l.ModuleID = m.ModuleID
                WHERE m.CourseID = @courseId AND ISNULL(l.IsDeleted,0) = 0 AND ISNULL(m.IsDeleted,0) = 0";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        public double GetProgressPercentage(Guid userId, Guid courseId)
        {
            int completed = GetCompletedLessonCount(userId, courseId);
            int total = GetTotalLessonCount(courseId);
            if (total == 0) return 0;
            return (double)completed / total * 100;
        }

        public List<Guid> GetCompletedLessonIds(Guid userId, Guid courseId)
        {
            var list = new List<Guid>();
            string sql = @"
                SELECT DISTINCT p.LessonID
                FROM Progress p
                INNER JOIN Lessons l ON p.LessonID = l.LessonID
                INNER JOIN Modules m ON l.ModuleID = m.ModuleID
                WHERE p.UserID = @userId AND m.CourseID = @courseId AND p.Status = 'completed'";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@userId", userId), new SqlParameter("@courseId", courseId));
            foreach (DataRow r in dt.Rows) if (r["LessonID"] != DBNull.Value) list.Add((Guid)r["LessonID"]);
            return list;
        }
    }
}