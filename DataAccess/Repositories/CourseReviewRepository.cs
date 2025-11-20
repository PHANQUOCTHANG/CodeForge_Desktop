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
                ReviewID = (Guid)row["ReviewID"],
                UserID = (Guid)row["UserID"],
                CourseID = (Guid)row["CourseID"],
                Rating = (int)row["Rating"],
                Comment = row["Comment"]?.ToString(),
                CreatedAt = (DateTime)row["CreatedAt"],
                UpdatedAt = row["UpdatedAt"] != DBNull.Value ? (DateTime?)row["UpdatedAt"] : null,
                IsDeleted = (bool)row["IsDeleted"]
            };
        }

        public CourseReview GetById(Guid id)
        {
            string sql = "SELECT * FROM CourseReviews WHERE ReviewID = @id AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@id", id));
            return dt.Rows.Count > 0 ? MapToReview(dt.Rows[0]) : null;
        }

        public List<CourseReview> GetByCourseId(Guid courseId)
        {
            var list = new List<CourseReview>();
            string sql = "SELECT * FROM CourseReviews WHERE CourseID = @courseId AND IsDeleted = 0 ORDER BY CreatedAt DESC";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            foreach (DataRow row in dt.Rows)
                list.Add(MapToReview(row));
            return list;
        }

        public CourseReview GetByUserAndCourse(Guid userId, Guid courseId)
        {
            string sql = "SELECT TOP 1 * FROM CourseReviews WHERE UserID = @userId AND CourseID = @courseId AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql,
                new SqlParameter("@userId", userId),
                new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? MapToReview(dt.Rows[0]) : null;
        }

        public int Add(CourseReview review)
        {
            if (review.ReviewID == Guid.Empty)
                review.ReviewID = Guid.NewGuid();

            string sql = @"
                INSERT INTO CourseReviews (ReviewID, UserID, CourseID, Rating, Comment, CreatedAt, IsDeleted)
                VALUES (@ReviewID, @UserID, @CourseID, @Rating, @Comment, @CreatedAt, 0)";

            return DbContext.Execute(sql,
                new SqlParameter("@ReviewID", review.ReviewID),
                new SqlParameter("@UserID", review.UserID),
                new SqlParameter("@CourseID", review.CourseID),
                new SqlParameter("@Rating", review.Rating),
                new SqlParameter("@Comment", (object)review.Comment ?? DBNull.Value),
                new SqlParameter("@CreatedAt", review.CreatedAt));
        }

        public int Update(CourseReview review)
        {
            review.UpdatedAt = DateTime.Now;
            string sql = @"
                UPDATE CourseReviews 
                SET Rating = @Rating, Comment = @Comment, UpdatedAt = @UpdatedAt
                WHERE ReviewID = @ReviewID AND IsDeleted = 0";

            return DbContext.Execute(sql,
                new SqlParameter("@ReviewID", review.ReviewID),
                new SqlParameter("@Rating", review.Rating),
                new SqlParameter("@Comment", (object)review.Comment ?? DBNull.Value),
                new SqlParameter("@UpdatedAt", review.UpdatedAt));
        }

        public int Delete(Guid id)
        {
            string sql = "UPDATE CourseReviews SET IsDeleted = 1 WHERE ReviewID = @id";
            return DbContext.Execute(sql, new SqlParameter("@id", id));
        }

        public double GetAverageRating(Guid courseId)
        {
            string sql = "SELECT AVG(CAST(Rating AS FLOAT)) FROM CourseReviews WHERE CourseID = @courseId AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                return (double)dt.Rows[0][0];
            return 0;
        }

        public int GetReviewCount(Guid courseId)
        {
            string sql = "SELECT COUNT(*) FROM CourseReviews WHERE CourseID = @courseId AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? (int)dt.Rows[0][0] : 0;
        }
    }
}