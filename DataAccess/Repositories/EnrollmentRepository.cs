using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private Enrollment MapToEnrollment(DataRow row)
        {
            return new Enrollment
            {
                EnrollmentID = (Guid)row["EnrollmentID"],
                UserID = (Guid)row["UserID"],
                CourseID = (Guid)row["CourseID"],
                EnrolledAt = (DateTime)row["EnrolledAt"],
                Status = row["Status"]?.ToString() ?? "Active",
                CompletedAt = row["CompletedAt"] != DBNull.Value ? (DateTime?)row["CompletedAt"] : null,
                IsDeleted = (bool)row["IsDeleted"]
            };
        }

        public Enrollment GetById(Guid id)
        {
            string sql = "SELECT * FROM Enrollments WHERE EnrollmentID = @id AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@id", id));
            return dt.Rows.Count > 0 ? MapToEnrollment(dt.Rows[0]) : null;
        }

        public List<Enrollment> GetByUserId(Guid userId)
        {
            var list = new List<Enrollment>();
            string sql = "SELECT * FROM Enrollments WHERE UserID = @userId AND IsDeleted = 0 ORDER BY EnrolledAt DESC";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@userId", userId));
            foreach (DataRow row in dt.Rows)
                list.Add(MapToEnrollment(row));
            return list;
        }

        public List<Enrollment> GetByCourseId(Guid courseId)
        {
            var list = new List<Enrollment>();
            string sql = "SELECT * FROM Enrollments WHERE CourseID = @courseId AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            foreach (DataRow row in dt.Rows)
                list.Add(MapToEnrollment(row));
            return list;
        }

        public Enrollment GetByUserAndCourse(Guid userId, Guid courseId)
        {
            string sql = "SELECT TOP 1 * FROM Enrollments WHERE UserID = @userId AND CourseID = @courseId AND IsDeleted = 0";
            DataTable dt = DbContext.Query(sql, 
                new SqlParameter("@userId", userId),
                new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? MapToEnrollment(dt.Rows[0]) : null;
        }

        public int Add(Enrollment enrollment)
        {
            if (enrollment.EnrollmentID == Guid.Empty)
                enrollment.EnrollmentID = Guid.NewGuid();

            string sql = @"
                INSERT INTO Enrollments (EnrollmentID, UserID, CourseID, EnrolledAt, Status, IsDeleted)
                VALUES (@EnrollmentID, @UserID, @CourseID, @EnrolledAt, @Status, 0)";

            return DbContext.Execute(sql,
                new SqlParameter("@EnrollmentID", enrollment.EnrollmentID),
                new SqlParameter("@UserID", enrollment.UserID),
                new SqlParameter("@CourseID", enrollment.CourseID),
                new SqlParameter("@EnrolledAt", enrollment.EnrolledAt),
                new SqlParameter("@Status", enrollment.Status));
        }

        public int Update(Enrollment enrollment)
        {
            string sql = @"
                UPDATE Enrollments 
                SET Status = @Status, CompletedAt = @CompletedAt
                WHERE EnrollmentID = @EnrollmentID AND IsDeleted = 0";

            return DbContext.Execute(sql,
                new SqlParameter("@EnrollmentID", enrollment.EnrollmentID),
                new SqlParameter("@Status", enrollment.Status),
                new SqlParameter("@CompletedAt", (object)enrollment.CompletedAt ?? DBNull.Value));
        }

        public int Delete(Guid id)
        {
            string sql = "UPDATE Enrollments SET IsDeleted = 1 WHERE EnrollmentID = @id";
            return DbContext.Execute(sql, new SqlParameter("@id", id));
        }

        public bool IsUserEnrolled(Guid userId, Guid courseId)
        {
            return GetByUserAndCourse(userId, courseId) != null;
        }

        public int GetEnrolledStudentCount(Guid courseId)
        {
            string sql = "SELECT COUNT(*) FROM Enrollments WHERE CourseID = @courseId AND IsDeleted = 0 AND Status = 'Active'";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            return dt.Rows.Count > 0 ? (int)dt.Rows[0][0] : 0;
        }
    }
}