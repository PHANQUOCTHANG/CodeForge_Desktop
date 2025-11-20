using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class SubmissionRepository : ISubmissionRepository
    {
        /// <summary>
        /// Tạo submission mới
        /// </summary>
        public bool Create(Submission submission)
        {
            try
            {
                string sql = @"
                    INSERT INTO Submissions 
                    (SubmissionID, UserID, ProblemID, Code, Language, Status, SubmitTime, ExecutionTime, MemoryUsed, QuantityTestPassed, QuantityTest, TestCaseIdFail)
                    VALUES 
                    (@SubmissionID, @UserID, @ProblemID, @Code, @Language, @Status, @SubmitTime, @ExecutionTime, @MemoryUsed, @QuantityTestPassed, @QuantityTest, @TestCaseIdFail)";

                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@SubmissionID", submission.SubmissionID),
                    new SqlParameter("@UserID", submission.UserID),
                    new SqlParameter("@ProblemID", submission.ProblemID),
                    new SqlParameter("@Code", submission.Code ?? ""),
                    new SqlParameter("@Language", submission.Language ?? ""),
                    new SqlParameter("@Status", submission.Status ?? ""),
                    new SqlParameter("@SubmitTime", submission.SubmitTime),
                    new SqlParameter("@ExecutionTime", submission.ExecutionTime ?? (object)DBNull.Value),
                    new SqlParameter("@MemoryUsed", submission.MemoryUsed ?? (object)DBNull.Value),
                    new SqlParameter("@QuantityTestPassed", submission.QuantityTestPassed ?? (object)DBNull.Value),
                    new SqlParameter("@QuantityTest", submission.QuantityTest ?? (object)DBNull.Value),
                    new SqlParameter("@TestCaseIdFail", submission.TestCaseIdFail ?? (object)DBNull.Value)
                };

                int result = DbContext.Execute(sql, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating submission: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy submission theo ID
        /// </summary>
        public Submission GetById(Guid submissionId)
        {
            try
            {
                string sql = @"
                    SELECT SubmissionID, UserID, ProblemID, Code, Language, Status, SubmitTime, 
                           ExecutionTime, MemoryUsed, QuantityTestPassed, QuantityTest, TestCaseIdFail
                    FROM Submissions
                    WHERE SubmissionID = @SubmissionID";

                SqlParameter[] parameters = new[] { new SqlParameter("@SubmissionID", submissionId) };
                DataTable dt = DbContext.Query(sql, parameters);

                if (dt.Rows.Count > 0)
                {
                    return MapToSubmission(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting submission: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Lấy tất cả submissions của user
        /// </summary>
        public List<Submission> GetByUserId(Guid userId)
        {
            var submissions = new List<Submission>();
            try
            {
                string sql = @"
                    SELECT SubmissionID, UserID, ProblemID, Code, Language, Status, SubmitTime, 
                           ExecutionTime, MemoryUsed, QuantityTestPassed, QuantityTest, TestCaseIdFail
                    FROM Submissions
                    WHERE UserID = @UserID
                    ORDER BY SubmitTime DESC";

                SqlParameter[] parameters = new[] { new SqlParameter("@UserID", userId) };
                DataTable dt = DbContext.Query(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    submissions.Add(MapToSubmission(row));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting submissions by user: {ex.Message}");
            }
            return submissions;
        }

        /// <summary>
        /// Lấy tất cả submissions của bài tập
        /// </summary>
        public List<Submission> GetByProblemId(Guid problemId)
        {
            var submissions = new List<Submission>();
            try
            {
                string sql = @"
                    SELECT SubmissionID, UserID, ProblemID, Code, Language, Status, SubmitTime, 
                           ExecutionTime, MemoryUsed, QuantityTestPassed, QuantityTest, TestCaseIdFail
                    FROM Submissions
                    WHERE ProblemID = @ProblemID
                    ORDER BY SubmitTime DESC";

                SqlParameter[] parameters = new[] { new SqlParameter("@ProblemID", problemId) };
                DataTable dt = DbContext.Query(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    submissions.Add(MapToSubmission(row));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting submissions by problem: {ex.Message}");
            }
            return submissions;
        }

        /// <summary>
        /// Lấy submissions của user cho một bài tập cụ thể
        /// </summary>
        public List<Submission> GetByUserAndProblem(Guid userId, Guid problemId)
        {
            var submissions = new List<Submission>();
            try
            {
                string sql = @"
                    SELECT SubmissionID, UserID, ProblemID, Code, Language, Status, SubmitTime, 
                           ExecutionTime, MemoryUsed, QuantityTestPassed, QuantityTest, TestCaseIdFail
                    FROM Submissions
                    WHERE UserID = @UserID AND ProblemID = @ProblemID
                    ORDER BY SubmitTime DESC";

                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@ProblemID", problemId)
                };

                DataTable dt = DbContext.Query(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    submissions.Add(MapToSubmission(row));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting submissions by user and problem: {ex.Message}");
            }
            return submissions;
        }

        /// <summary>
        /// Cập nhật submission
        /// </summary>
        public bool Update(Submission submission)
        {
            try
            {
                string sql = @"
                    UPDATE Submissions
                    SET Code = @Code, Language = @Language, Status = @Status, 
                        ExecutionTime = @ExecutionTime, MemoryUsed = @MemoryUsed, 
                        QuantityTestPassed = @QuantityTestPassed, QuantityTest = @QuantityTest, 
                        TestCaseIdFail = @TestCaseIdFail
                    WHERE SubmissionID = @SubmissionID";

                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@SubmissionID", submission.SubmissionID),
                    new SqlParameter("@Code", submission.Code ?? ""),
                    new SqlParameter("@Language", submission.Language ?? ""),
                    new SqlParameter("@Status", submission.Status ?? ""),
                    new SqlParameter("@ExecutionTime", submission.ExecutionTime ?? (object)DBNull.Value),
                    new SqlParameter("@MemoryUsed", submission.MemoryUsed ?? (object)DBNull.Value),
                    new SqlParameter("@QuantityTestPassed", submission.QuantityTestPassed ?? (object)DBNull.Value),
                    new SqlParameter("@QuantityTest", submission.QuantityTest ?? (object)DBNull.Value),
                    new SqlParameter("@TestCaseIdFail", submission.TestCaseIdFail ?? (object)DBNull.Value)
                };

                int result = DbContext.Execute(sql, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating submission: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa submission
        /// </summary>
        public bool Delete(Guid submissionId)
        {
            try
            {
                string sql = "DELETE FROM Submissions WHERE SubmissionID = @SubmissionID";
                SqlParameter[] parameters = new[] { new SqlParameter("@SubmissionID", submissionId) };

                int result = DbContext.Execute(sql, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting submission: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy tất cả submissions
        /// </summary>
        public List<Submission> GetAll()
        {
            var submissions = new List<Submission>();
            try
            {
                string sql = @"
                    SELECT SubmissionID, UserID, ProblemID, Code, Language, Status, SubmitTime, 
                           ExecutionTime, MemoryUsed, QuantityTestPassed, QuantityTest, TestCaseIdFail
                    FROM Submissions
                    ORDER BY SubmitTime DESC";

                DataTable dt = DbContext.Query(sql);

                foreach (DataRow row in dt.Rows)
                {
                    submissions.Add(MapToSubmission(row));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all submissions: {ex.Message}");
            }
            return submissions;
        }

        /// <summary>
        /// Map DataRow thành Submission object
        /// </summary>
        private Submission MapToSubmission(DataRow row)
        {
            return new Submission
            {
                SubmissionID = (Guid)row["SubmissionID"],
                UserID = (Guid)row["UserID"],
                ProblemID = (Guid)row["ProblemID"],
                Code = row["Code"].ToString(),
                Language = row["Language"].ToString(),
                Status = row["Status"].ToString(),
                SubmitTime = (DateTime)row["SubmitTime"],
                ExecutionTime = row["ExecutionTime"] == DBNull.Value ? (int?)null : (int)row["ExecutionTime"],
                MemoryUsed = row["MemoryUsed"] == DBNull.Value ? (int?)null : (int)row["MemoryUsed"],
                QuantityTestPassed = row["QuantityTestPassed"] == DBNull.Value ? (int?)null : (int)row["QuantityTestPassed"],
                QuantityTest = row["QuantityTest"] == DBNull.Value ? (int?)null : (int)row["QuantityTest"],
                TestCaseIdFail = row["TestCaseIdFail"] == DBNull.Value ? (Guid?)null : (Guid)row["TestCaseIdFail"]
            };
        }
    }
}