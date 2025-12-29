using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.Business.Helpers; // Dùng AppLogger
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        // Helper check cột tồn tại (để tương thích ngược với DB cũ/mới)
        private bool ColumnExists(string tableName, string columnName)
        {
            try
            {
                var sql = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @t AND COLUMN_NAME = @c";
                var dt = DbContext.Query(sql, new SqlParameter("@t", tableName), new SqlParameter("@c", columnName));
                return dt != null && dt.Rows.Count > 0;
            }
            catch { return false; }
        }

        // Helper Map
        private Enrollment MapToEnrollment(DataRow row)
        {
            return new Enrollment
            {
                EnrollmentID = (Guid)row["EnrollmentID"],
                UserID = (Guid)row["UserID"],
                CourseID = (Guid)row["CourseID"],
                EnrolledAt = row["EnrolledAt"] != DBNull.Value ? (DateTime)row["EnrolledAt"] : DateTime.MinValue,
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "enrolled",
                // IsDeleted map nếu có cột
            };
        }

        public Enrollment GetById(Guid id)
        {
            string sql = "SELECT TOP 1 * FROM Enrollments WHERE EnrollmentID = @id";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@id", id));
            return dt.Rows.Count > 0 ? MapToEnrollment(dt.Rows[0]) : null;
        }

        public List<Enrollment> GetByUserId(Guid userId)
        {
            var list = new List<Enrollment>();
            string sql = "SELECT * FROM Enrollments WHERE UserID = @userId";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@userId", userId));
            if (dt != null) foreach (DataRow r in dt.Rows) list.Add(MapToEnrollment(r));
            return list;
        }

        public List<Enrollment> GetByCourseId(Guid courseId)
        {
            var list = new List<Enrollment>();
            string sql = "SELECT * FROM Enrollments WHERE CourseID = @courseId";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
            if (dt != null) foreach (DataRow r in dt.Rows) list.Add(MapToEnrollment(r));
            return list;
        }

        // --- NEW: Get Detail ---
        public Enrollment GetByUserIdAndCourseId(Guid userId, Guid courseId)
        {
            string sql = "SELECT TOP 1 * FROM Enrollments WHERE UserID = @uId AND CourseID = @cId";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@uId", userId), new SqlParameter("@cId", courseId));
            return dt.Rows.Count > 0 ? MapToEnrollment(dt.Rows[0]) : null;
        }

        public bool Exists(Guid userId, Guid courseId)
        {
            string sql = "SELECT COUNT(1) FROM Enrollments WHERE UserID = @uId AND CourseID = @cId";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@uId", userId), new SqlParameter("@cId", courseId));
            return dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public bool IsUserEnrolled(Guid userId, Guid courseId)
        {
            return Exists(userId, courseId);
        }

        public int GetEnrolledStudentCount(Guid courseId)
        {
            string sql = "SELECT COUNT(*) FROM Enrollments WHERE CourseID = @cId";
            DataTable dt = DbContext.Query(sql, new SqlParameter("@cId", courseId));
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        // --- CRUD ---

        public int Add(Enrollment e)
        {
            if (e.EnrollmentID == Guid.Empty) e.EnrollmentID = Guid.NewGuid();
            if (e.EnrolledAt == DateTime.MinValue) e.EnrolledAt = DateTime.Now;

            string sql = @"INSERT INTO Enrollments (EnrollmentID, UserID, CourseID, EnrolledAt, Status) 
                           VALUES (@Id, @UserId, @CourseId, @Date, @Status)";

            return DbContext.Execute(sql,
                new SqlParameter("@Id", e.EnrollmentID),
                new SqlParameter("@UserId", e.UserID),
                new SqlParameter("@CourseId", e.CourseID),
                new SqlParameter("@Date", e.EnrolledAt),
                new SqlParameter("@Status", e.Status ?? "enrolled")
            );
        }

        public int Update(Enrollment e)
        {
            string sql = "UPDATE Enrollments SET Status = @Status WHERE EnrollmentID = @Id";
            return DbContext.Execute(sql, new SqlParameter("@Status", e.Status), new SqlParameter("@Id", e.EnrollmentID));
        }

        public int Delete(Guid id)
        {
            // Hard Delete cho đơn giản, hoặc Soft Delete nếu DB có cột IsDeleted
            string sql = "DELETE FROM Enrollments WHERE EnrollmentID = @Id";
            return DbContext.Execute(sql, new SqlParameter("@Id", id));
        }
    }
}