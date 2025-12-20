using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class TestCaseRepository : ITestCaseRepository
    {
        private TestCase MapToTestCase(DataRow row)
        {
            if (row == null) return null;

            return new TestCase
            {
                TestCaseID = row["TestCaseID"] != DBNull.Value ? Guid.Parse(row["TestCaseID"].ToString()) : Guid.Empty,
                ProblemID = row["ProblemID"] != DBNull.Value ? Guid.Parse(row["ProblemID"].ToString()) : Guid.Empty,
                Input = row["Input"]?.ToString(),
                ExpectedOutput = row["ExpectedOutput"]?.ToString(),
                Explain = row["Explain"]?.ToString(),
                IsHidden = row["IsHidden"] != DBNull.Value ? Convert.ToBoolean(row["IsHidden"]) : false,
                IsDeleted = row["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(row["IsDeleted"]) : false
            };
        }

        public TestCase GetById(Guid id)
        {
            string sql = "SELECT * FROM TestCases WHERE TestCaseID = @id AND IsDeleted = 0";
            var param = new SqlParameter("@id", id);
            DataTable dt = DbContext.Query(sql, param);
            return dt.Rows.Count > 0 ? MapToTestCase(dt.Rows[0]) : null;
        }

        public List<TestCase> GetByProblemId(Guid problemId)
        {
            var list = new List<TestCase>();
            string sql = "SELECT * FROM TestCases WHERE ProblemID = @problemId AND IsDeleted = 0 ORDER BY TestCaseID";
            var param = new SqlParameter("@problemId", problemId);
            DataTable dt = DbContext.Query(sql, param);
            foreach (DataRow r in dt.Rows) list.Add(MapToTestCase(r));
            return list;
        }

        

        public List<TestCase> GetVisibleByProblemId(Guid problemId)
        {
            var list = new List<TestCase>();
            string sql = "SELECT TOP 3 * FROM TestCases WHERE ProblemID = @problemId AND IsHidden = 1 AND IsDeleted = 0 ORDER BY TestCaseID ";
            var param = new SqlParameter("@problemId", problemId);
            DataTable dt = DbContext.Query(sql, param);
            foreach (DataRow r in dt.Rows) list.Add(MapToTestCase(r));
            return list;
        }

        public List<TestCase> GetAll()
        {
            var list = new List<TestCase>();
            string sql = "SELECT * FROM TestCases WHERE IsDeleted = 0 ORDER BY ProblemID, TestCaseID";
            DataTable dt = DbContext.Query(sql);
            foreach (DataRow r in dt.Rows) list.Add(MapToTestCase(r));
            return list;
        }

        

        public int Add(TestCase testCase)
        {
            string sql = @"
                INSERT INTO TestCases
                (TestCaseID, ProblemID, Input, ExpectedOutput, Explain, IsHidden, IsDeleted)
                VALUES
                (@TestCaseID, @ProblemID, @Input, @ExpectedOutput, @Explain, @IsHidden, @IsDeleted)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TestCaseID", testCase.TestCaseID),
                new SqlParameter("@ProblemID", testCase.ProblemID),
                new SqlParameter("@Input", testCase.Input ?? (object)DBNull.Value),
                new SqlParameter("@ExpectedOutput", testCase.ExpectedOutput ?? (object)DBNull.Value),
                new SqlParameter("@Explain", testCase.Explain ?? (object)DBNull.Value),
                new SqlParameter("@IsHidden", testCase.IsHidden),
                new SqlParameter("@IsDeleted", testCase.IsDeleted)
            };

            return DbContext.Execute(sql, parameters);
        }

        public int Update(TestCase testCase)
        {
            string sql = @"
                UPDATE TestCases SET
                    Input = @Input,
                    ExpectedOutput = @ExpectedOutput,
                    Explain = @Explain,
                    IsHidden = @IsHidden
                WHERE TestCaseID = @TestCaseID AND IsDeleted = 0";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Input", testCase.Input ?? (object)DBNull.Value),
                new SqlParameter("@ExpectedOutput", testCase.ExpectedOutput ?? (object)DBNull.Value),
                new SqlParameter("@Explain", testCase.Explain ?? (object)DBNull.Value),
                new SqlParameter("@IsHidden", testCase.IsHidden),
                new SqlParameter("@TestCaseID", testCase.TestCaseID)
            };

            return DbContext.Execute(sql, parameters);
        }

        public int Delete(Guid id)
        {
            string sql = "UPDATE TestCases SET IsDeleted = 1 WHERE TestCaseID = @id";
            var param = new SqlParameter("@id", id);
            return DbContext.Execute(sql, param);
        }
    }
}