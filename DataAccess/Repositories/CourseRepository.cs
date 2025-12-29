using CodeForge_Desktop.Business.DTOs;
using CodeForge_Desktop.Config;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private T SafeGet<T>(DataRow row, string colName, T defaultValue = default)
        {
            if (!row.Table.Columns.Contains(colName)) return defaultValue;
            object value = row[colName];
            if (value == DBNull.Value || value == null) return defaultValue;
            try
            {
                if (typeof(T) == typeof(Guid)) return (T)(object)Guid.Parse(value.ToString());
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch { return defaultValue; }
        }

        private Course MapToCourse(DataRow row)
        {
            return new Course
            {
                // Sửa CourseId -> CourseID
                CourseID = SafeGet<Guid>(row, "CourseID"),
                Title = SafeGet<string>(row, "Title"),
                Description = SafeGet<string>(row, "Description"),
                Overview = SafeGet<string>(row, "Overview"),
                Level = SafeGet<string>(row, "Level"),
                Language = SafeGet<string>(row, "Language"),
                Price = SafeGet<decimal>(row, "Price"),
                Discount = SafeGet<decimal>(row, "Discount"),
                Thumbnail = SafeGet<string>(row, "Thumbnail"),
                Status = SafeGet<string>(row, "Status"),
                CreatedAt = SafeGet<DateTime>(row, "CreatedAt"),
                LessonCount = SafeGet<int>(row, "LessonCount"),
                Duration = SafeGet<int>(row, "Duration"),
                Rating = SafeGet<double>(row, "Rating"),
                TotalStudents = SafeGet<int>(row, "TotalStudents"),
                IsEnrolled = row.Table.Columns.Contains("IsEnrolled") ? (SafeGet<int>(row, "IsEnrolled") == 1) : false,
                ProgressPercentage = row.Table.Columns.Contains("ProgressPercentage") ? SafeGet<int>(row, "ProgressPercentage") : 0
            };
        }

        public async Task<List<Course>> GetAllAsync(string search = null, string level = null)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT * FROM Courses WHERE IsDeleted = 0";
                var parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(search)) { sql += " AND Title LIKE @Search"; parameters.Add(new SqlParameter("@Search", "%" + search + "%")); }
                if (!string.IsNullOrEmpty(level) && level != "All" && level != "Tất cả level") { sql += " AND Level = @Level"; parameters.Add(new SqlParameter("@Level", level)); }

                sql += " ORDER BY CreatedAt DESC";
                DataTable dt = DbContext.Query(sql, parameters.ToArray());

                var list = new List<Course>();
                if (dt != null) foreach (DataRow r in dt.Rows) list.Add(MapToCourse(r));
                return list;
            });
        }

        public async Task<List<Course>> GetListHasEnrollAsync(Guid userId)
        {
            return await Task.Run(() =>
            {
                string sql = @"
            SELECT c.*, 
                   1 AS IsEnrolled, -- Vì đã JOIN nên chắc chắn là đã Enroll, set luôn = 1
                   (SELECT COUNT(DISTINCT p.LessonID) * 100 / NULLIF(c.LessonCount, 0) 
                    FROM Progress p 
                    JOIN Lessons l ON p.LessonID = l.LessonID 
                    JOIN Modules m ON l.ModuleID = m.ModuleID
                    WHERE p.UserID = @UserId AND m.CourseID = c.CourseID AND p.Status = 'completed') AS ProgressPercentage
            FROM Courses c
            JOIN Enrollments e ON c.CourseID = e.CourseID -- SỬA LEFT JOIN THÀNH JOIN
            WHERE c.IsDeleted = 0 
              AND c.Status = 'active'
              AND e.UserID = @UserId -- ĐK lọc User nằm ở đây
            ORDER BY e.EnrolledAt DESC"; // Sắp xếp theo ngày đăng ký mới nhất (thường hợp lý hơn CreatedAt của khóa học)

                DataTable dt = DbContext.Query(sql, new SqlParameter("@UserId", userId));
                var list = new List<Course>();
                if (dt != null) foreach (DataRow r in dt.Rows) list.Add(MapToCourse(r));
                return list;
            });
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT TOP 1 * FROM Courses WHERE CourseID = @Id";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@Id", id));
                if (dt != null && dt.Rows.Count > 0) return MapToCourse(dt.Rows[0]);
                return null;
            });
        }

        public async Task AddAsync(Course c)
        {
            await Task.Run(() =>
            {
                string sql = @"INSERT INTO Courses (CourseID, Title, Description, Overview, Level, Language, Price, Discount, Thumbnail, Status, CreatedAt, IsDeleted, Slug, LessonCount, Duration) 
                               VALUES (@Id, @Title, @Desc, @Over, @Lvl, @Lang, @Price, @Disc, @Thumb, @Status, @Created, 0, @Slug, 0, 0)";
                DbContext.Execute(sql,
                    new SqlParameter("@Id", c.CourseID), // Sửa
                    new SqlParameter("@Title", c.Title), new SqlParameter("@Desc", (object)c.Description ?? DBNull.Value),
                    new SqlParameter("@Over", (object)c.Overview ?? DBNull.Value), new SqlParameter("@Lvl", (object)c.Level ?? DBNull.Value),
                    new SqlParameter("@Lang", (object)c.Language ?? DBNull.Value), new SqlParameter("@Price", c.Price), new SqlParameter("@Disc", c.Discount),
                    new SqlParameter("@Thumb", (object)c.Thumbnail ?? DBNull.Value), new SqlParameter("@Status", c.Status ?? "draft"),
                    new SqlParameter("@Created", c.CreatedAt), new SqlParameter("@Slug", c.Slug)
                );
            });
        }

        public async Task UpdateAsync(Course c)
        {
            await Task.Run(() =>
            {
                string sql = @"UPDATE Courses SET Title=@Title, Description=@Desc, Overview=@Over, Level=@Lvl, Language=@Lang, 
                               Price=@Price, Discount=@Disc, Thumbnail=@Thumb, Status=@Status, LessonCount=@LCount, Duration=@Dur
                               WHERE CourseID=@Id";
                DbContext.Execute(sql,
                    new SqlParameter("@Title", c.Title), new SqlParameter("@Desc", (object)c.Description ?? DBNull.Value),
                    new SqlParameter("@Over", (object)c.Overview ?? DBNull.Value), new SqlParameter("@Lvl", (object)c.Level ?? DBNull.Value),
                    new SqlParameter("@Lang", (object)c.Language ?? DBNull.Value), new SqlParameter("@Price", c.Price),
                    new SqlParameter("@Disc", c.Discount), new SqlParameter("@Thumb", (object)c.Thumbnail ?? DBNull.Value),
                    new SqlParameter("@Status", c.Status), new SqlParameter("@LCount", c.LessonCount),
                    new SqlParameter("@Dur", c.Duration),
                    new SqlParameter("@Id", c.CourseID) // Sửa
                );
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            await Task.Run(() => DbContext.Execute("UPDATE Courses SET IsDeleted = 1 WHERE CourseID = @Id", new SqlParameter("@Id", id)));
        }

        public async Task<bool> ExistsByTitleAsync(string title, Guid? excludeId = null)
        {
            return await Task.Run(() => {
                string sql = "SELECT COUNT(1) FROM Courses WHERE Title = @Title";
                var p = new List<SqlParameter> { new SqlParameter("@Title", title) };
                if (excludeId.HasValue) { sql += " AND CourseID != @Ex"; p.Add(new SqlParameter("@Ex", excludeId.Value)); }
                DataTable dt = DbContext.Query(sql, p.ToArray());
                return dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0;
            });
        }

        public async Task<bool> ExistsBySlugAsync(string slug)
        {
            return await Task.Run(() => {
                DataTable dt = DbContext.Query("SELECT COUNT(1) FROM Courses WHERE Slug = @Slug", new SqlParameter("@Slug", slug));
                return dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0;
            });
        }

        // --- MODULES ---

        public async Task<List<Module>> GetModulesByCourseIdAsync(Guid courseId)
        {
            return await Task.Run(() => {
                string sql = "SELECT * FROM Modules WHERE CourseID = @Id AND IsDeleted = 0 ORDER BY OrderIndex";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@Id", courseId));
                var list = new List<Module>();
                if (dt != null) foreach (DataRow r in dt.Rows)
                    {
                        list.Add(new Module
                        {
                            ModuleID = SafeGet<Guid>(r, "ModuleID"), // Sửa
                            CourseID = SafeGet<Guid>(r, "CourseID"), // Sửa
                            Title = SafeGet<string>(r, "Title"),
                            OrderIndex = SafeGet<int>(r, "OrderIndex")
                        });
                    }
                return list;
            });
        }

        public async Task AddModuleAsync(Module m)
        {
            await Task.Run(() => DbContext.Execute("INSERT INTO Modules (ModuleID, CourseID, Title, OrderIndex, IsDeleted) VALUES (@Id, @CId, @Title, @Order, 0)",
                new SqlParameter("@Id", m.ModuleID), new SqlParameter("@CId", m.CourseID), new SqlParameter("@Title", m.Title), new SqlParameter("@Order", m.OrderIndex)));
        }

        public async Task UpdateModuleAsync(Module m)
        {
            await Task.Run(() => DbContext.Execute("UPDATE Modules SET Title = @Title, OrderIndex = @Order WHERE ModuleID = @Id",
                new SqlParameter("@Title", m.Title), new SqlParameter("@Order", m.OrderIndex), new SqlParameter("@Id", m.ModuleID)));
        }

        public async Task DeleteModuleAsync(Guid id)
        {
            await Task.Run(() => DbContext.Execute("UPDATE Modules SET IsDeleted = 1 WHERE ModuleID = @Id", new SqlParameter("@Id", id)));
        }

        // --- LESSONS ---

        public async Task<List<Lesson>> GetLessonsByModuleIdAsync(Guid moduleId)
        {
            return await Task.Run(() => {
                string sql = "SELECT * FROM Lessons WHERE ModuleID = @Id AND IsDeleted = 0 ORDER BY OrderIndex";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@Id", moduleId));
                var list = new List<Lesson>();
                if (dt != null) foreach (DataRow r in dt.Rows)
                    {
                        list.Add(new Lesson
                        {
                            LessonID = SafeGet<Guid>(r, "LessonID"), // Sửa
                            ModuleID = SafeGet<Guid>(r, "ModuleID"), // Sửa
                            Title = SafeGet<string>(r, "Title"),
                            LessonType = SafeGet<string>(r, "LessonType"),
                            Duration = SafeGet<int>(r, "Duration"),
                            OrderIndex = SafeGet<int>(r, "OrderIndex")
                        });
                    }
                return list;
            });
        }

        public async Task AddLessonAsync(Lesson l)
        {
            await Task.Run(() => DbContext.Execute("INSERT INTO Lessons (LessonID, ModuleID, Title, LessonType, Duration, OrderIndex, IsDeleted) VALUES (@Id, @MId, @Title, @Type, @Dur, @Order, 0)",
                new SqlParameter("@Id", l.LessonID), new SqlParameter("@MId", l.ModuleID), new SqlParameter("@Title", l.Title),
                new SqlParameter("@Type", l.LessonType), new SqlParameter("@Dur", l.Duration), new SqlParameter("@Order", l.OrderIndex)));
        }

        public async Task UpdateLessonAsync(Lesson l)
        {
            await Task.Run(() => DbContext.Execute("UPDATE Lessons SET Title=@Title, LessonType=@Type, Duration=@Dur, OrderIndex=@Order WHERE LessonID=@Id",
                new SqlParameter("@Title", l.Title), new SqlParameter("@Type", l.LessonType), new SqlParameter("@Dur", l.Duration),
                new SqlParameter("@Order", l.OrderIndex), new SqlParameter("@Id", l.LessonID)));
        }

        public async Task DeleteLessonAsync(Guid id)
        {
            await Task.Run(() => DbContext.Execute("UPDATE Lessons SET IsDeleted = 1 WHERE LessonID = @Id", new SqlParameter("@Id", id)));
        }

        // --- CONTENT ---

        public async Task<LessonVideo> GetVideoByLessonIdAsync(Guid lessonId)
        {
            return await Task.Run(() => {
                DataTable dt = DbContext.Query("SELECT * FROM LessonVideos WHERE LessonID = @Id", new SqlParameter("@Id", lessonId));
                if (dt != null && dt.Rows.Count > 0) return new LessonVideo { LessonID = lessonId, VideoUrl = SafeGet<string>(dt.Rows[0], "VideoUrl"), Duration = SafeGet<int>(dt.Rows[0], "Duration") };
                return null;
            });
        }

        public async Task AddOrUpdateVideoAsync(LessonVideo v)
        {
            await Task.Run(() => {
                var check = DbContext.Query("SELECT 1 FROM LessonVideos WHERE LessonID = @Id", new SqlParameter("@Id", v.LessonID));
                if (check != null && check.Rows.Count > 0)
                    DbContext.Execute("UPDATE LessonVideos SET VideoUrl = @Url, Duration = @Dur WHERE LessonID = @Id", new SqlParameter("@Url", v.VideoUrl), new SqlParameter("@Dur", v.Duration), new SqlParameter("@Id", v.LessonID));
                else
                    DbContext.Execute("INSERT INTO LessonVideos (LessonID, VideoUrl, Duration) VALUES (@Id, @Url, @Dur)", new SqlParameter("@Id", v.LessonID), new SqlParameter("@Url", v.VideoUrl), new SqlParameter("@Dur", v.Duration));
            });
        }

        

        public async Task AddOrUpdateTextAsync(Guid lessonId, string content)
        {
            await Task.Run(() => {
                var check = DbContext.Query("SELECT 1 FROM LessonTexts WHERE LessonID = @Id", new SqlParameter("@Id", lessonId));
                if (check != null && check.Rows.Count > 0)
                    DbContext.Execute("UPDATE LessonTexts SET Content = @Content WHERE LessonID = @Id", new SqlParameter("@Content", content), new SqlParameter("@Id", lessonId));
                else
                    DbContext.Execute("INSERT INTO LessonTexts (LessonID, Content) VALUES (@Id, @Content)", new SqlParameter("@Id", lessonId), new SqlParameter("@Content", content));
            });
        }

        public async Task RemoveContentAsync(Guid lessonId, string lessonType)
        {
            await Task.Run(() => {
                string table = lessonType.ToLower() switch { "video" => "LessonVideos", "text" => "LessonTexts", "quiz" => "LessonQuizzes", "coding" => "CodingProblems", _ => null };
                if (table != null) DbContext.Execute($"DELETE FROM {table} WHERE LessonID = @Id", new SqlParameter("@Id", lessonId));
            });
        }
        public async Task<LessonVideoDto?> GetVideoContentAsync(Guid lessonId)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT VideoUrl, Caption FROM LessonVideos WHERE LessonID = @LID";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@LID", lessonId));

                if (dt != null && dt.Rows.Count > 0)
                {
                    return new LessonVideoDto
                    {
                        VideoUrl = dt.Rows[0]["VideoUrl"]?.ToString() ?? "",
                        Caption = dt.Rows[0]["Caption"]?.ToString()
                    };
                }
                return null;
            });
        }

        // 2. Lấy Text Content
        public async Task<LessonTextDto?> GetTextContentAsync(Guid lessonId)
        {
            return await Task.Run(() =>
            {
                string sql = "SELECT Content FROM LessonTexts WHERE LessonID = @LID";
                DataTable dt = DbContext.Query(sql, new SqlParameter("@LID", lessonId));

                if (dt != null && dt.Rows.Count > 0)
                {
                    return new LessonTextDto
                    {
                        Content = dt.Rows[0]["Content"]?.ToString() ?? ""
                    };
                }
                return null;
            });
        }

        // 3. Lấy Quiz Content (Bao gồm Quiz Header + List Questions)
        public async Task<LessonQuizDto?> GetQuizContentAsync(Guid lessonId)
        {
            return await Task.Run(() =>
            {
                // A. Lấy thông tin bài Quiz (Title, Description) từ bảng LessonQuizzes
                string sqlQuiz = "SELECT Title, Description FROM LessonQuizzes WHERE LessonID = @LID";
                DataTable dtQuiz = DbContext.Query(sqlQuiz, new SqlParameter("@LID", lessonId));

                if (dtQuiz == null || dtQuiz.Rows.Count == 0) return null;

                var quizDto = new LessonQuizDto
                {
                    Title = dtQuiz.Rows[0]["Title"]?.ToString() ?? "Quiz",
                    Description = dtQuiz.Rows[0]["Description"]?.ToString(),
                    Questions = new List<QuizQuestionDto>()
                };

                // B. Lấy danh sách câu hỏi từ bảng QuizQuestions
                // Lưu ý: Trong DB, QuizQuestions liên kết với LessonQuizzes qua LessonQuizId (chính là LessonID)
                string sqlQuestions = @"
                    SELECT QuestionID, Question, Answers, Explanation, CorrectIndex 
                    FROM QuizQuestions 
                    WHERE LessonQuizId = @LID";

                DataTable dtQuestions = DbContext.Query(sqlQuestions, new SqlParameter("@LID", lessonId));

                if (dtQuestions != null)
                {
                    foreach (DataRow row in dtQuestions.Rows)
                    {
                        string[] answers = Array.Empty<string>();
                        try
                        {
                            // Parse JSON string ["A", "B"] thành mảng string[]
                            string json = row["Answers"]?.ToString();
                            if (!string.IsNullOrEmpty(json))
                            {
                                answers = JsonSerializer.Deserialize<string[]>(json) ?? Array.Empty<string>();
                            }
                        }
                        catch { /* Ignore json error */ }

                        quizDto.Questions.Add(new QuizQuestionDto
                        {
                            QuestionID = (Guid)row["QuestionID"],
                            Question = row["Question"]?.ToString() ?? "",
                            Answers = answers,
                            Explanation = row["Explanation"]?.ToString() ?? "",
                            CorrectIndex = row["CorrectIndex"] != DBNull.Value ? Convert.ToInt32(row["CorrectIndex"]) : 0
                        });
                    }
                }

                return quizDto;
            });
        }

        // 4. Lấy Coding Problem
        public async Task<ProblemDto?> GetCodingProblemAsync(Guid lessonId)
        {
            return await Task.Run(() =>
            {
                string sql = @"
                    SELECT ProblemID, Title, Slug, Difficulty, Description, 
                           TimeLimit, MemoryLimit, FunctionName 
                    FROM CodingProblems 
                    WHERE LessonID = @LID AND IsDeleted = 0";

                DataTable dt = DbContext.Query(sql, new SqlParameter("@LID", lessonId));

                if (dt != null && dt.Rows.Count > 0)
                {
                    var r = dt.Rows[0];
                    // Tạo code mẫu giả định (hoặc lấy từ DB nếu có cột InitialCode)
                    string funcName = r["FunctionName"]?.ToString() ?? "solve";
                    string initialCode = $"// Write your code here\npublic class Solution {{\n    public void {funcName}() {{\n        \n    }}\n}}";

                    return new ProblemDto
                    {
                        ProblemId = (Guid)r["ProblemID"],
                        Title = r["Title"]?.ToString() ?? "",
                        Slug = r["Slug"]?.ToString() ?? "",
                        Difficulty = r["Difficulty"]?.ToString() ?? "Easy",
                        Description = r["Description"]?.ToString(),
                        TimeLimit = r["TimeLimit"] != DBNull.Value ? Convert.ToInt32(r["TimeLimit"]) : 1000,
                        MemoryLimit = r["MemoryLimit"] != DBNull.Value ? Convert.ToInt32(r["MemoryLimit"]) : 256,
                        //InitialCode = initialCode
                    };
                }
                return null;
            });
        }
    }
}