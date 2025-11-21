using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class CourseReviewRepository : ICourseReviewRepository
    {
        private CourseReview MapToReview(DataRow row)
        {
            return new CourseReview
            {
                ReviewID = row.Table.Columns.Contains("ReviewID") && row["ReviewID"] != DBNull.Value ? (Guid)row["ReviewID"] : Guid.Empty,
                UserID = row.Table.Columns.Contains("UserID") && row["UserID"] != DBNull.Value ? (Guid)row["UserID"] : Guid.Empty,
                CourseID = row.Table.Columns.Contains("CourseID") && row["CourseID"] != DBNull.Value ? (Guid)row["CourseID"] : Guid.Empty,
                Rating = row.Table.Columns.Contains("Rating") && row["Rating"] != DBNull.Value ? (int)row["Rating"] : 0,
                Comment = row.Table.Columns.Contains("Comment") ? row["Comment"]?.ToString() : null,
                CreatedAt = row.Table.Columns.Contains("CreatedAt") && row["CreatedAt"] != DBNull.Value ? (DateTime)row["CreatedAt"] : default(DateTime),
                UpdatedAt = row.Table.Columns.Contains("UpdatedAt") && row["UpdatedAt"] != DBNull.Value ? (DateTime)row["UpdatedAt"] : default(DateTime)
            };
        }

        public CourseReview GetById(Guid id)
        {
            string sql = "SELECT TOP 1 ReviewID, UserID, CourseID, Rating, Comment, CreatedAt, UpdatedAt FROM CourseReviews WHERE ReviewID = @id";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@id", id));
            return dt != null && dt.Rows.Count > 0 ? MapToReview(dt.Rows[0]) : null;
        }

        public List<CourseReview> GetByCourseId(Guid courseId)
        {
            var list = new List<CourseReview>();
            string sql = "SELECT ReviewID, UserID, CourseID, Rating, Comment, CreatedAt, UpdatedAt FROM CourseReviews WHERE CourseID = @courseId ORDER BY CreatedAt DESC";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                    list.Add(MapToReview(row));
            }
            return list;
        }

        public CourseReview GetByUserAndCourse(Guid userId, Guid courseId)
        {
            string sql = "SELECT TOP 1 ReviewID, UserID, CourseID, Rating, Comment, CreatedAt, UpdatedAt FROM CourseReviews WHERE UserID = @userId AND CourseID = @courseId ORDER BY CreatedAt DESC";
            DataTable dt = DbContext.Query(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@courseId", courseId));
            return dt != null && dt.Rows.Count > 0 ? MapToReview(dt.Rows[0]) : null;
        }

        public int Add(CourseReview review)
        {
            if (review.ReviewID == Guid.Empty) review.ReviewID = Guid.NewGuid();
            if (review.CreatedAt == default(DateTime)) review.CreatedAt = DateTime.UtcNow;
            if (review.UpdatedAt == default(DateTime)) review.UpdatedAt = review.CreatedAt;

            string sql = @"
                INSERT INTO CourseReviews (ReviewID, UserID, CourseID, Rating, Comment, CreatedAt, UpdatedAt)
                VALUES (@ReviewID, @UserID, @CourseID, @Rating, @Comment, @CreatedAt, @UpdatedAt)";
            return DbContext.Execute(sql,
                new SqlParameter("@ReviewID", review.ReviewID),
                new SqlParameter("@UserID", review.UserID),
                new SqlParameter("@CourseID", review.CourseID),
                new SqlParameter("@Rating", review.Rating),
                new SqlParameter("@Comment", (object)review.Comment ?? DBNull.Value),
                new SqlParameter("@CreatedAt", review.CreatedAt),
                new SqlParameter("@UpdatedAt", review.UpdatedAt));
        }

        public int Update(CourseReview review)
        {
            review.UpdatedAt = DateTime.UtcNow;
            string sql = @"
                UPDATE CourseReviews 
                SET Rating = @Rating, Comment = @Comment, UpdatedAt = @UpdatedAt
                WHERE ReviewID = @ReviewID";
            return DbContext.Execute(sql,
                new SqlParameter("@ReviewID", review.ReviewID),
                new SqlParameter("@Rating", review.Rating),
                new SqlParameter("@Comment", (object)review.Comment ?? DBNull.Value),
                new SqlParameter("@UpdatedAt", review.UpdatedAt));
        }

        public int Delete(Guid id)
        {
            // Your schema does not have soft-delete for CourseReviews; perform hard delete.
            string sql = "DELETE FROM CourseReviews WHERE ReviewID = @id";
            return DbContext.Execute(sql, new SqlParameter("@id", id));
        }

        public double GetAverageRating(Guid courseId)
        {
            string sql = "SELECT AVG(CAST(Rating AS FLOAT)) FROM CourseReviews WHERE CourseID = @courseId";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                return Convert.ToDouble(dt.Rows[0][0]);
            return 0;
        }

        public int GetReviewCount(Guid courseId)
        {
            string sql = "SELECT COUNT(*) FROM CourseReviews WHERE CourseID = @courseId";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }
    }
}