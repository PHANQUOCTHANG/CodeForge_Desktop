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
                ProgressID = (Guid)row["ProgressID"],
                UserID = (Guid)row["UserID"],
                CourseID = (Guid)row["CourseID"],
                LessonID = (Guid)row["LessonID"],
                IsCompleted = (bool)row["IsCompleted"],
                CompletedAt = row["CompletedAt"] != DBNull.Value ? (DateTime?)row["CompletedAt"] : null,
                ProgressPercentage = row["ProgressPercentage"] != DBNull.Value ? (int)row["ProgressPercentage"] : 0,
                CreatedAt = (DateTime)row["CreatedAt"],
                UpdatedAt = row["UpdatedAt"] != DBNull.Value ? (DateTime?)row["UpdatedAt"] : null,
                IsDeleted = (bool)row["IsDeleted"]
            };
        }

        public Progress GetById(Guid id)
        {
            string sql = "SELECT * FROM Progress WHERE ProgressID = @id AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@id", id));
            return dt.Rows.Count > 0 ? MapToProgress(dt.Rows[0]) : null;
        }

        public List<Progress> GetByUserAndCourse(Guid userId, Guid courseId)
        {
            var list = new List<Progress>();
            string sql = "SELECT * FROM Progress WHERE UserID = @userId AND CourseID = @courseId AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@courseId", courseId));
            foreach (DataRow row in dt.Rows)
                list.Add(MapToProgress(row));
            return list;
        }

        public Progress GetByUserLessonAndCourse(Guid userId, Guid lessonId, Guid courseId)
        {
            string sql = "SELECT TOP 1 * FROM Progress WHERE UserID = @userId AND LessonID = @lessonId AND CourseID = @courseId AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@lessonId", lessonId),
                new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? MapToProgress(dt.Rows[0]) : null;
        }

        public int Add(Progress progress)
        {
            if (progress.ProgressID == Guid.Empty)
                progress.ProgressID = Guid.NewGuid();

            string sql = @"
                INSERT INTO Progress (ProgressID, UserID, CourseID, LessonID, IsCompleted, CompletedAt, ProgressPercentage, CreatedAt, IsDeleted)
                VALUES (@ProgressID, @UserID, @CourseID, @LessonID, @IsCompleted, @CompletedAt, @ProgressPercentage, @CreatedAt, 0)";

            return DbContext.Execute(sql,
                new SqlParameter("@ProgressID", progress.ProgressID),
                new SqlParameter("@UserID", progress.UserID),
                new SqlParameter("@CourseID", progress.CourseID),
                new SqlParameter("@LessonID", progress.LessonID),
                new SqlParameter("@IsCompleted", progress.IsCompleted),
                new SqlParameter("@CompletedAt", (object)progress.CompletedAt ?? DBNull.Value),
                new SqlParameter("@ProgressPercentage", progress.ProgressPercentage),
                new SqlParameter("@CreatedAt", progress.CreatedAt));
        }

        public int Update(Progress progress)
        {
            progress.UpdatedAt = DateTime.Now;
            string sql = @"
                UPDATE Progress 
                SET IsCompleted = @IsCompleted, CompletedAt = @CompletedAt, ProgressPercentage = @ProgressPercentage, UpdatedAt = @UpdatedAt
                WHERE ProgressID = @ProgressID AND IsDeleted = 0";

            return DbContext.Execute(sql,
                new SqlParameter("@ProgressID", progress.ProgressID),
                new SqlParameter("@IsCompleted", progress.IsCompleted),
                new SqlParameter("@CompletedAt", (object)progress.CompletedAt ?? DBNull.Value),
                new SqlParameter("@ProgressPercentage", progress.ProgressPercentage),
                new SqlParameter("@UpdatedAt", progress.UpdatedAt));
        }

        public int Delete(Guid id)
        {
            string sql = "UPDATE Progress SET IsDeleted = 1 WHERE ProgressID = @id";
            return DbContext.Execute(sql, new SqlParameter("@id", id));
        }

        public int GetCompletedLessonCount(Guid userId, Guid courseId)
        {
            string sql = "SELECT COUNT(*) FROM Progress WHERE UserID = @userId AND CourseID = @courseId AND IsCompleted = 1 AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? (int)dt.Rows[0][0] : 0;
        }

        public int GetTotalLessonCount(Guid courseId)
        {
            string sql = @"
                SELECT COUNT(DISTINCT l.LessonID) 
                FROM Lessons l
                WHERE l.CourseID = (SELECT CourseID FROM Modules WHERE ModuleID IN (SELECT ModuleID FROM Lessons WHERE CourseID IN (SELECT CourseID FROM Courses WHERE CourseID = @courseId)))
                AND l.IsDeleted = 0";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? (int)dt.Rows[0][0] : 0;
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
            string sql = "SELECT LessonID FROM Progress WHERE UserID = @userId AND CourseID = @courseId AND IsCompleted = 1 AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@courseId", courseId));
            
            var list = new List<Guid>();
            foreach (DataRow row in dt.Rows)
                if (row["LessonID"] != DBNull.Value)
                    list.Add((Guid)row["LessonID"]);
            return list;
        }
    }
}