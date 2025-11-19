
using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class CodingProblemRepository : ICodingProblemRepository
    {
        private CodingProblem MapToCodingProblem(DataRow row)
        {
            if (row == null) return null;

            return new CodingProblem
            {
                ProblemID = row["ProblemID"] != DBNull.Value ? Guid.Parse(row["ProblemID"].ToString()) : Guid.Empty,
                LessonID = row["LessonID"] != DBNull.Value ? (Guid?)Guid.Parse(row["LessonID"].ToString()) : null,
                Title = row["Title"]?.ToString(),
                Slug = row["Slug"]?.ToString(),
                Difficulty = row["Difficulty"]?.ToString(),
                Status = row["Status"]?.ToString(),
                Description = row["Description"]?.ToString(),
                Tags = row["Tags"]?.ToString(),
                FunctionName = row["FunctionName"]?.ToString(),
                Parameters = row["Parameters"]?.ToString(),
                ReturnType = row["ReturnType"]?.ToString(),
                Notes = row["Notes"]?.ToString(),
                Constraints = row["Constraints"]?.ToString(),
                TimeLimit = row["TimeLimit"] != DBNull.Value ? Convert.ToInt32(row["TimeLimit"]) : 1000,
                MemoryLimit = row["MemoryLimit"] != DBNull.Value ? Convert.ToInt32(row["MemoryLimit"]) : 256,
                CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : DateTime.Now,
                UpdatedAt = row["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedAt"]) : DateTime.Now,
                IsDeleted = row["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(row["IsDeleted"]) : false
            };
        }

        public CodingProblem GetById(Guid id)
        {
            string sql = "SELECT * FROM CodingProblems WHERE ProblemID = @id AND IsDeleted = 0";
            var param = new SqlParameter("@id", id);
            DataTable dt = DbContext.Query(sql, param);
            return dt.Rows.Count > 0 ? MapToCodingProblem(dt.Rows[0]) : null;
        }

        public List<CodingProblem> GetAll()
        {
            var list = new List<CodingProblem>();
            string sql = "SELECT * FROM CodingProblems WHERE IsDeleted = 0 ORDER BY CreatedAt DESC";
            DataTable dt = DbContext.Query(sql);
            foreach (DataRow r in dt.Rows) list.Add(MapToCodingProblem(r));
            return list;
        }

        

        public List<CodingProblem> GetByLessonId(Guid? lessonId)
        {
            var list = new List<CodingProblem>();
            string sql;
            SqlParameter[] parameters;

            if (lessonId.HasValue)
            {
                sql = "SELECT * FROM CodingProblems WHERE LessonID = @lessonId AND IsDeleted = 0 ORDER BY CreatedAt DESC";
                parameters = new[] { new SqlParameter("@lessonId", lessonId.Value) };
            }
            else
            {
                sql = "SELECT * FROM CodingProblems WHERE LessonID IS NULL AND IsDeleted = 0 ORDER BY CreatedAt DESC";
                parameters = new SqlParameter[0];
            }

            DataTable dt = parameters.Length > 0 ? DbContext.Query(sql, parameters) : DbContext.Query(sql);
            foreach (DataRow r in dt.Rows) list.Add(MapToCodingProblem(r));
            return list;
        }

        public CodingProblem GetBySlug(string slug)
        {
            string sql = "SELECT * FROM CodingProblems WHERE Slug = @slug AND IsDeleted = 0";
            var param = new SqlParameter("@slug", slug ?? (object)DBNull.Value);
            DataTable dt = DbContext.Query(sql, param);
            return dt.Rows.Count > 0 ? MapToCodingProblem(dt.Rows[0]) : null;
        }

        public int Add(CodingProblem problem)
        {
            string sql = @"
                INSERT INTO CodingProblems
                (ProblemID, LessonID, Title, Slug, Difficulty, Status, Description, Tags, FunctionName, Parameters, ReturnType, Notes, Constraints, TimeLimit, MemoryLimit, CreatedAt, UpdatedAt, IsDeleted)
                VALUES
                (@ProblemID, @LessonID, @Title, @Slug, @Difficulty, @Status, @Description, @Tags, @FunctionName, @Parameters, @ReturnType, @Notes, @Constraints, @TimeLimit, @MemoryLimit, @CreatedAt, @UpdatedAt, @IsDeleted)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProblemID", problem.ProblemID),
                new SqlParameter("@LessonID", problem.LessonID ?? (object)DBNull.Value),
                new SqlParameter("@Title", problem.Title ?? (object)DBNull.Value),
                new SqlParameter("@Slug", problem.Slug ?? (object)DBNull.Value),
                new SqlParameter("@Difficulty", problem.Difficulty ?? (object)DBNull.Value),
                new SqlParameter("@Status", problem.Status ?? (object)DBNull.Value),
                new SqlParameter("@Description", problem.Description ?? (object)DBNull.Value),
                new SqlParameter("@Tags", problem.Tags ?? (object)DBNull.Value),
                new SqlParameter("@FunctionName", problem.FunctionName ?? (object)DBNull.Value),
                new SqlParameter("@Parameters", problem.Parameters ?? (object)DBNull.Value),
                new SqlParameter("@ReturnType", problem.ReturnType ?? (object)DBNull.Value),
                new SqlParameter("@Notes", problem.Notes ?? (object)DBNull.Value),
                new SqlParameter("@Constraints", problem.Constraints ?? (object)DBNull.Value),
                new SqlParameter("@TimeLimit", problem.TimeLimit),
                new SqlParameter("@MemoryLimit", problem.MemoryLimit),
                new SqlParameter("@CreatedAt", problem.CreatedAt),
                new SqlParameter("@UpdatedAt", problem.UpdatedAt),
                new SqlParameter("@IsDeleted", problem.IsDeleted)
            };

            return DbContext.Execute(sql, parameters);
        }

        public int Update(CodingProblem problem)
        {
            string sql = @"
                UPDATE CodingProblems SET
                    LessonID = @LessonID,
                    Title = @Title,
                    Slug = @Slug,
                    Difficulty = @Difficulty,
                    Status = @Status,
                    Description = @Description,
                    Tags = @Tags,
                    FunctionName = @FunctionName,
                    Parameters = @Parameters,
                    ReturnType = @ReturnType,
                    Notes = @Notes,
                    Constraints = @Constraints,
                    TimeLimit = @TimeLimit,
                    MemoryLimit = @MemoryLimit,
                    UpdatedAt = @UpdatedAt
                WHERE ProblemID = @ProblemID AND IsDeleted = 0";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LessonID", problem.LessonID ?? (object)DBNull.Value),
                new SqlParameter("@Title", problem.Title ?? (object)DBNull.Value),
                new SqlParameter("@Slug", problem.Slug ?? (object)DBNull.Value),
                new SqlParameter("@Difficulty", problem.Difficulty ?? (object)DBNull.Value),
                new SqlParameter("@Status", problem.Status ?? (object)DBNull.Value),
                new SqlParameter("@Description", problem.Description ?? (object)DBNull.Value),
                new SqlParameter("@Tags", problem.Tags ?? (object)DBNull.Value),
                new SqlParameter("@FunctionName", problem.FunctionName ?? (object)DBNull.Value),
                new SqlParameter("@Parameters", problem.Parameters ?? (object)DBNull.Value),
                new SqlParameter("@ReturnType", problem.ReturnType ?? (object)DBNull.Value),
                new SqlParameter("@Notes", problem.Notes ?? (object)DBNull.Value),
                new SqlParameter("@Constraints", problem.Constraints ?? (object)DBNull.Value),
                new SqlParameter("@TimeLimit", problem.TimeLimit),
                new SqlParameter("@MemoryLimit", problem.MemoryLimit),
                new SqlParameter("@UpdatedAt", DateTime.Now),
                new SqlParameter("@ProblemID", problem.ProblemID)
            };

            return DbContext.Execute(sql, parameters);
        }

        public int Delete(Guid id)
        {
            string sql = "UPDATE CodingProblems SET IsDeleted = 1, Status = N'NOT_STARTED' WHERE ProblemID = @id";
            var param = new SqlParameter("@id", id);
            return DbContext.Execute(sql, param);
        }
    }
}