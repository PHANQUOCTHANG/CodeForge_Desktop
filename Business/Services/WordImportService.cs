using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using CodeForge_Desktop.Business.Helpers;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;

namespace CodeForge_Desktop.Business.Services
{
    /// <summary>
    /// Service để import bài lập trình từ file Word (.docx)
    /// </summary>
    public class WordImportService
    {
        private readonly ICodingProblemRepository _problemRepository;
        private readonly ITestCaseRepository _testCaseRepository;
        private List<string> _importLog;
        private int _successCount;
        private int _failureCount;

        public WordImportService(ICodingProblemRepository problemRepository, ITestCaseRepository testCaseRepository)
        {
            _problemRepository = problemRepository ?? throw new ArgumentNullException(nameof(problemRepository));
            _testCaseRepository = testCaseRepository ?? throw new ArgumentNullException(nameof(testCaseRepository));
            _importLog = new List<string>();
            _successCount = 0;
            _failureCount = 0;
        }

        /// <summary>
        /// Import bài lập trình từ file Word
        /// </summary>
        /// <param name="filePath">Đường dẫn đến file Word</param>
        /// <param name="lessonId">ID của bài học (optional)</param>
        /// <returns>Kết quả import (số bài thành công, số bài lỗi, log chi tiết)</returns>
        public ImportResult ImportFromWordFile(string filePath, Guid? lessonId = null)
        {
            _importLog.Clear();
            _successCount = 0;
            _failureCount = 0;

            var result = new ImportResult();

            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"File không tồn tại: {filePath}");

                if (!filePath.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("Chỉ hỗ trợ file .docx (Word)");

                // Đọc nội dung từ file Word
                string wordContent = ExtractTextFromWord(filePath);

                if (string.IsNullOrWhiteSpace(wordContent))
                {
                    LogError("File Word rỗng hoặc không thể đọc");
                    result.Message = "File Word rỗng hoặc không thể đọc";
                    return result;
                }

                LogInfo($"Đã đọc {wordContent.Length} ký tự từ file Word");

                // Tách nhiều bài (PROBLEM block)
                var problems = WordParsingHelper.SplitProblems(wordContent);
                LogInfo($"Tìm thấy {problems.Count} bài lập trình");

                if (problems.Count == 0)
                {
                    LogError("Không tìm thấy bài nào trong file (format: === PROBLEM === ... === END ===)");
                    result.Message = "Không tìm thấy bài nào trong file";
                    return result;
                }

                // Import từng bài - ✅ Sửa: dùng for loop thay vì foreach
                for (int i = 0; i < problems.Count; i++)
                {
                    try
                    {
                        ImportProblem(problems[i], i + 1, lessonId, result);
                    }
                    catch (Exception ex)
                    {
                        _failureCount++;
                        LogError($"Lỗi khi import bài #{i + 1}: {ex.Message}");
                    }
                }

                result.SuccessCount = _successCount;
                result.FailureCount = _failureCount;
                result.Log = _importLog;
                result.Message = $"Import thành công {_successCount} bài, {_failureCount} bài lỗi";

                return result;
            }
            catch (Exception ex)
            {
                LogError($"Lỗi chung: {ex.Message}");
                result.Message = ex.Message;
                result.Log = _importLog;
                return result;
            }
        }

        /// <summary>
        /// Import một bài lập trình
        /// </summary>
        private void ImportProblem(string problemContent, int problemIndex, Guid? lessonId, ImportResult result)
        {
            // Trích xuất các field
            string title = WordParsingHelper.ExtractField("TITLE", problemContent, "").Trim();
            string slug = WordParsingHelper.ExtractField("SLUG", problemContent, "").Trim();
            string difficulty = WordParsingHelper.ExtractField("DIFFICULTY", problemContent, "Dễ").Trim();
            string status = WordParsingHelper.ExtractField("STATUS", problemContent, "NOT_STARTED").Trim();
            string description = WordParsingHelper.ExtractField("DESCRIPTION", problemContent, "").Trim();
            string tags = WordParsingHelper.ExtractField("TAGS", problemContent, "").Trim();
            string functionName = WordParsingHelper.ExtractField("FUNCTION_NAME", problemContent, "").Trim();
            string parameters = WordParsingHelper.ExtractField("PARAMETERS", problemContent, "").Trim();
            string returnType = WordParsingHelper.ExtractField("RETURN_TYPE", problemContent, "").Trim();
            string notes = WordParsingHelper.ExtractField("NOTES", problemContent, "").Trim();
            string constraints = WordParsingHelper.ExtractField("CONSTRAINTS", problemContent, "").Trim();
            
            int timeLimit = WordParsingHelper.ParseInt(
                WordParsingHelper.ExtractField("TIME_LIMIT", problemContent, ""), 1000);
            int memoryLimit = WordParsingHelper.ParseInt(
                WordParsingHelper.ExtractField("MEMORY_LIMIT", problemContent, ""), 256);

            // Validation: TITLE bắt buộc
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidOperationException("Bài lập trình phải có TITLE");

            // Auto-generate slug nếu không có
            if (string.IsNullOrWhiteSpace(slug))
                slug = WordParsingHelper.GenerateSlug(title);

            // Đảm bảo slug unique
            slug = EnsureUniqueSlug(slug);

            // Tách testcase
            var testcases = WordParsingHelper.SplitTestCases(problemContent);

            // Validation: ít nhất 1 testcase
            if (testcases.Count == 0)
                throw new InvalidOperationException($"Bài '{title}' phải có ít nhất 1 TESTCASE");

            // Tạo CodingProblem
            var problem = new CodingProblem
            {
                ProblemID = Guid.NewGuid(),
                LessonID = lessonId,
                Title = title,
                Slug = slug,
                Difficulty = difficulty,
                Status = status,
                Description = description,
                Tags = tags,
                FunctionName = functionName,
                Parameters = parameters,
                ReturnType = returnType,
                Notes = notes,
                Constraints = constraints,
                TimeLimit = timeLimit,
                MemoryLimit = memoryLimit,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            };

            // Lưu bài vào DB
            int problemSaveResult = _problemRepository.Add(problem);
            if (problemSaveResult <= 0)
                throw new InvalidOperationException("Không thể lưu bài vào database");

            LogInfo($"✓ Bài #{problemIndex} '{title}' đã lưu thành công (ID: {problem.ProblemID})");

            // Import testcase - ✅ Sửa: dùng for loop thay vì foreach
            int testcaseCount = 0;
            for (int i = 0; i < testcases.Count; i++)
            {
                try
                {
                    string input = WordParsingHelper.ExtractField("INPUT", testcases[i], "").Trim();
                    string expectedOutput = WordParsingHelper.ExtractField("OUTPUT", testcases[i], "").Trim();
                    string explain = WordParsingHelper.ExtractField("EXPLAIN", testcases[i], "").Trim();
                    bool isHidden = WordParsingHelper.ParseBool(
                        WordParsingHelper.ExtractField("HIDDEN", testcases[i], "0"));

                    var testcase = new TestCase
                    {
                        TestCaseID = Guid.NewGuid(),
                        ProblemID = problem.ProblemID,
                        Input = input,
                        ExpectedOutput = expectedOutput,
                        Explain = explain,
                        IsHidden = isHidden,
                        IsDeleted = false
                    };

                    int tcSaveResult = _testCaseRepository.Add(testcase);
                    if (tcSaveResult > 0)
                    {
                        testcaseCount++;
                        LogInfo($"  ├─ Testcase #{i + 1} thêm thành công");
                    }
                }
                catch (Exception ex)
                {
                    LogError($"  ├─ Lỗi testcase #{i + 1}: {ex.Message}");
                }
            }

            LogInfo($"  └─ Tổng: {testcaseCount}/{testcases.Count} testcase");

            _successCount++;
        }

        /// <summary>
        /// Đảm bảo slug là unique bằng cách thêm số nếu cần
        /// </summary>
        private string EnsureUniqueSlug(string slug)
        {
            var existing = _problemRepository.GetBySlug(slug);
            if (existing == null)
                return slug;

            // Slug đã tồn tại, thêm suffix
            int counter = 1;
            while (true)
            {
                string newSlug = $"{slug}-{counter}";
                existing = _problemRepository.GetBySlug(newSlug);
                if (existing == null)
                    return newSlug;
                counter++;
            }
        }

        /// <summary>
        /// Đọc nội dung text từ file Word (.docx)
        /// </summary>
        private string ExtractTextFromWord(string filePath)
        {
            var text = new StringBuilder();
            int retries = 3;
            int delayMs = 500;

            while (retries > 0)
            {
                try
                {
                    // Mở file ở chế độ read-only để tránh lock
                    using (var doc = WordprocessingDocument.Open(filePath, false))
                    {
                        if (doc.MainDocumentPart?.Document?.Body == null)
                            return string.Empty;

                        var body = doc.MainDocumentPart.Document.Body;

                        foreach (var element in body.ChildElements)
                        {
                            if (element is DocumentFormat.OpenXml.Wordprocessing.Paragraph para)
                            {
                                text.AppendLine(ExtractTextFromParagraph(para));
                            }
                            else if (element is DocumentFormat.OpenXml.Wordprocessing.Table table)
                            {
                                text.AppendLine(ExtractTextFromTable(table));
                            }
                        }
                    }

                    return text.ToString();
                }
                catch (System.IO.IOException ex) when (ex.Message.Contains("being used"))
                {
                    retries--;
                    if (retries == 0)
                    {
                        throw new InvalidOperationException(
                            $"Không thể mở file Word. Vui lòng đóng file trong Microsoft Word trước.\n\nLỗi: {ex.Message}", ex);
                    }

                    // Chờ một chút rồi thử lại
                    System.Threading.Thread.Sleep(delayMs);
                    LogError($"File bị lock, thử lại... (Lần {4 - retries}/3)");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Lỗi khi đọc file Word: {ex.Message}", ex);
                }
            }

            return text.ToString();
        }

        /// <summary>
        /// Trích xuất text từ paragraph
        /// </summary>
        private string ExtractTextFromParagraph(DocumentFormat.OpenXml.Wordprocessing.Paragraph para)
        {
            var text = new StringBuilder();

            foreach (var run in para.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>())
            {
                foreach (var element in run.ChildElements)
                {
                    if (element is DocumentFormat.OpenXml.Wordprocessing.Text t && t.Text != null)
                        text.Append(t.Text);
                }
            }

            return text.ToString();
        }

        /// <summary>
        /// Trích xuất text từ table
        /// </summary>
        private string ExtractTextFromTable(DocumentFormat.OpenXml.Wordprocessing.Table table)
        {
            var text = new StringBuilder();

            foreach (var row in table.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableRow>())
            {
                foreach (var cell in row.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableCell>())
                {
                    foreach (var para in cell.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
                    {
                        text.Append(ExtractTextFromParagraph(para));
                        text.Append(" ");
                    }
                }
                text.AppendLine();
            }

            return text.ToString();
        }

        /// <summary>
        /// Log thông tin
        /// </summary>
        private void LogInfo(string message)
        {
            _importLog.Add($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
        }

        /// <summary>
        /// Log lỗi
        /// </summary>
        private void LogError(string message)
        {
            _importLog.Add($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
        }
    }

    /// <summary>
    /// Kết quả của quá trình import
    /// </summary>
    public class ImportResult
    {
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public string Message { get; set; }
        public List<string> Log { get; set; } = new List<string>();
    }
}