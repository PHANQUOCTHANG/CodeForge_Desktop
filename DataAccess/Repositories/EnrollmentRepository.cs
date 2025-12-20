using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private bool ColumnExists(string tableName, string columnName)
        {
            try
            {
                var sql = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @t AND COLUMN_NAME = @c";
                var dt = DbContext.Query(sql, new SqlParameter("@t", tableName), new SqlParameter("@c", columnName));
                return dt != null && dt.Rows.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        private Enrollment MapToEnrollment(DataRow row)
        {
            Guid GetGuid(string name) => row.Table.Columns.Contains(name) && row[name] != DBNull.Value ? (Guid)row[name] : Guid.Empty;
            DateTime? GetDate(string name) => row.Table.Columns.Contains(name) && row[name] != DBNull.Value ? (DateTime?)row[name] : null;
            string GetString(string name) => row.Table.Columns.Contains(name) && row[name] != DBNull.Value ? row[name].ToString() : null;

            return new Enrollment
            {
                EnrollmentID = GetGuid("EnrollmentID"),
                UserID = GetGuid("UserID"),
                CourseID = GetGuid("CourseID"),
                EnrolledAt = GetDate("EnrolledAt") ?? DateTime.MinValue,
                Status = GetString("Status") ?? "enrolled",
            };
        }

        public Enrollment GetById(Guid id)
        {
            try
            {
                string sql = "SELECT TOP 1 * FROM Enrollments WHERE EnrollmentID = @id";
                // if IsDeleted column exists, filter it
                if (ColumnExists("Enrollments", "IsDeleted"))
                    sql = "SELECT TOP 1 * FROM Enrollments WHERE EnrollmentID = @id AND ISNULL(IsDeleted, 0) = 0";

                DataTable dt = DbContext.Query(sql, new SqlParameter("@id", id));
                return dt.Rows.Count > 0 ? MapToEnrollment(dt.Rows[0]) : null;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(GetById));
                throw;
            }
        }

        public List<Enrollment> GetByUserId(Guid userId)
        {
            var list = new List<Enrollment>();
            try
            {
                string sql = "SELECT * FROM Enrollments WHERE UserID = @userId ORDER BY EnrolledAt DESC";
                if (ColumnExists("Enrollments", "IsDeleted"))
                    sql = "SELECT * FROM Enrollments WHERE UserID = @userId AND ISNULL(IsDeleted, 0) = 0 ORDER BY EnrolledAt DESC";

                DataTable dt = DbContext.Query(sql, new SqlParameter("@userId", userId));
                foreach (DataRow row in dt.Rows)
                    list.Add(MapToEnrollment(row));
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(GetByUserId));
            }
            return list;
        }

        public List<Enrollment> GetByCourseId(Guid courseId)
        {
            var list = new List<Enrollment>();
            try
            {
                string sql = "SELECT * FROM Enrollments WHERE CourseID = @courseId";
                if (ColumnExists("Enrollments", "IsDeleted"))
                    sql = "SELECT * FROM Enrollments WHERE CourseID = @courseId AND ISNULL(IsDeleted, 0) = 0";

                DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
                foreach (DataRow row in dt.Rows)
                    list.Add(MapToEnrollment(row));
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(GetByCourseId));
            }
            return list;
        }

        public Enrollment GetByUserAndCourse(Guid userId, Guid courseId)
        {
            try
            {
                string sql = "SELECT TOP 1 * FROM Enrollments WHERE UserID = @userId AND CourseID = @courseId";
                if (ColumnExists("Enrollments", "IsDeleted"))
                    sql = "SELECT TOP 1 * FROM Enrollments WHERE UserID = @userId AND CourseID = @courseId AND ISNULL(IsDeleted, 0) = 0";

                DataTable dt = DbContext.Query(sql,
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@courseId", courseId));
                return dt.Rows.Count > 0 ? MapToEnrollment(dt.Rows[0]) : null;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(GetByUserAndCourse));
                throw;
            }
        }

        public int Add(Enrollment enrollment)
        {
            try
            {
                if (enrollment.EnrollmentID == Guid.Empty)
                    enrollment.EnrollmentID = Guid.NewGuid();

                // Do NOT assume IsDeleted column exists in DB — insert only existing columns
                if (ColumnExists("Enrollments", "IsDeleted"))
                {
                    string sql = @"
                        INSERT INTO Enrollments (EnrollmentID, UserID, CourseID, EnrolledAt, Status, IsDeleted)
                        VALUES (@EnrollmentID, @USERID, @CourseID, @EnrolledAt, @Status, 0)";
                    return DbContext.Execute(sql,
                        new SqlParameter("@EnrollmentID", enrollment.EnrollmentID),
                        new SqlParameter("@USERID", enrollment.UserID),
                        new SqlParameter("@CourseID", enrollment.CourseID),
                        new SqlParameter("@EnrolledAt", enrollment.EnrolledAt),
                        new SqlParameter("@Status", (object)enrollment.Status ?? "enrolled"));
                }
                else
                {
                    string sql = @"
                        INSERT INTO Enrollments (EnrollmentID, UserID, CourseID, EnrolledAt, Status)
                        VALUES (@EnrollmentID, @USERID, @CourseID, @EnrolledAt, @Status)";
                    return DbContext.Execute(sql,
                        new SqlParameter("@EnrollmentID", enrollment.EnrollmentID),
                        new SqlParameter("@USERID", enrollment.UserID),
                        new SqlParameter("@CourseID", enrollment.CourseID),
                        new SqlParameter("@EnrolledAt", enrollment.EnrolledAt),
                        new SqlParameter("@Status", (object)enrollment.Status ?? "enrolled"));
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(Add));
                throw;
            }
        }

        public int Update(Enrollment enrollment)
        {
            try
            {
                // Only update columns that exist. CompletedAt may not exist.
                bool hasCompletedAt = ColumnExists("Enrollments", "CompletedAt");
                string sql = hasCompletedAt
                    ? @"UPDATE Enrollments SET Status = @Status, CompletedAt = @CompletedAt WHERE EnrollmentID = @EnrollmentID"
                    : @"UPDATE Enrollments SET Status = @Status WHERE EnrollmentID = @EnrollmentID";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@EnrollmentID", enrollment.EnrollmentID),
                    new SqlParameter("@Status", (object)enrollment.Status ?? "enrolled")
                };
                

                return DbContext.Execute(sql, parameters.ToArray());
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(Update));
                throw;
            }
        }

        public int Delete(Guid id)
        {
            try
            {
                // Prefer soft-delete if column exists, otherwise hard-delete
                if (ColumnExists("Enrollments", "IsDeleted"))
                {
                    string sql = "UPDATE Enrollments SET IsDeleted = 1 WHERE EnrollmentID = @id";
                    return DbContext.Execute(sql, new SqlParameter("@id", id));
                }
                else
                {
                    string sql = "DELETE FROM Enrollments WHERE EnrollmentID = @id";
                    return DbContext.Execute(sql, new SqlParameter("@id", id));
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(Delete));
                throw;
            }
        }

        public bool IsUserEnrolled(Guid userId, Guid courseId)
        {
            try
            {
                return GetByUserAndCourse(userId, courseId) != null;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(IsUserEnrolled));
                return false;
            }
        }

        public int GetEnrolledStudentCount(Guid courseId)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM Enrollments WHERE CourseID = @courseId";
                if (ColumnExists("Enrollments", "IsDeleted"))
                    sql = "SELECT COUNT(*) FROM Enrollments WHERE CourseID = @courseId AND ISNULL(IsDeleted,0) = 0";

                DataTable dt = DbContext.Query(sql, new SqlParameter("@courseId", courseId));
                return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(GetEnrolledStudentCount));
                return 0;
            }
        }
    }
}