using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CodeForge_Desktop.Business.Helpers
{
    /// <summary>
    /// Helper class để parse nội dung từ Word và trích xuất thông tin
    /// </summary>
    public static class WordParsingHelper
    {
        /// <summary>
        /// Tách nhiều bài (PROBLEM block) từ toàn bộ nội dung
        /// </summary>
        public static List<string> SplitProblems(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return new List<string>();

            var problems = new List<string>();
            var pattern = @"===\s*PROBLEM\s*===(.*?)===\s*END\s*===";
            var matches = Regex.Matches(content, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    problems.Add(match.Groups[1].Value.Trim());
                }
            }

            return problems;
        }

        /// <summary>
        /// Tách nhiều TESTCASE từ nội dung của một bài
        /// </summary>
        public static List<string> SplitTestCases(string problemContent)
        {
            if (string.IsNullOrWhiteSpace(problemContent))
                return new List<string>();

            var testcases = new List<string>();
            var pattern = @"TESTCASE:(.*?)(?=TESTCASE:|$)";
            var matches = Regex.Matches(problemContent, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    testcases.Add(match.Groups[1].Value.Trim());
                }
            }

            return testcases;
        }

        /// <summary>
        /// Trích xuất giá trị của một field từ text
        /// Ví dụ: ExtractField("TITLE:", content) -> "Tính tổng hai số"
        /// </summary>
        public static string ExtractField(string fieldName, string content, string defaultValue = "")
        {
            if (string.IsNullOrWhiteSpace(content))
                return defaultValue;

            // Tìm field name (case-insensitive)
            var pattern = $@"{Regex.Escape(fieldName)}\s*:?\s*(.+?)(?=\n[A-Z_]+:|$)";
            var match = Regex.Match(content, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value.Trim();
            }

            return defaultValue;
        }

        /// <summary>
        /// Auto-generate slug từ title
        /// Ví dụ: "Tính tổng hai số" -> "tinh-tong-hai-so"
        /// </summary>
        public static string GenerateSlug(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Guid.NewGuid().ToString().Substring(0, 8);

            // Loại bỏ dấu
            string text = RemoveAccents(title.ToLower().Trim());

            // Chỉ giữ lại chữ, số, và dấu gạch ngang
            text = Regex.Replace(text, @"[^\w\s-]", "");

            // Thay thế khoảng trắng bằng gạch ngang
            text = Regex.Replace(text, @"\s+", "-");

            // Xóa gạch ngang thừa
            text = Regex.Replace(text, @"-+", "-");

            // Xóa gạch ngang ở đầu và cuối
            text = text.Trim('-');

            // Giới hạn độ dài
            if (text.Length > 100)
                text = text.Substring(0, 100).TrimEnd('-');

            return string.IsNullOrWhiteSpace(text) ? Guid.NewGuid().ToString().Substring(0, 8) : text;
        }

        /// <summary>
        /// Loại bỏ dấu từ text tiếng Việt
        /// </summary>
        private static string RemoveAccents(string text)
        {
            var accents = new Dictionary<char, char>
            {
                { 'à', 'a' }, { 'á', 'a' }, { 'ả', 'a' }, { 'ã', 'a' }, { 'ạ', 'a' },
                { 'ă', 'a' }, { 'ằ', 'a' }, { 'ắ', 'a' }, { 'ẳ', 'a' }, { 'ẵ', 'a' }, { 'ặ', 'a' },
                { 'â', 'a' }, { 'ầ', 'a' }, { 'ấ', 'a' }, { 'ẩ', 'a' }, { 'ẫ', 'a' }, { 'ậ', 'a' },
                { 'đ', 'd' },
                { 'è', 'e' }, { 'é', 'e' }, { 'ẻ', 'e' }, { 'ẽ', 'e' }, { 'ẹ', 'e' },
                { 'ê', 'e' }, { 'ề', 'e' }, { 'ế', 'e' }, { 'ể', 'e' }, { 'ễ', 'e' }, { 'ệ', 'e' },
                { 'ì', 'i' }, { 'í', 'i' }, { 'ỉ', 'i' }, { 'ĩ', 'i' }, { 'ị', 'i' },
                { 'ò', 'o' }, { 'ó', 'o' }, { 'ỏ', 'o' }, { 'õ', 'o' }, { 'ọ', 'o' },
                { 'ô', 'o' }, { 'ồ', 'o' }, { 'ố', 'o' }, { 'ổ', 'o' }, { 'ỗ', 'o' }, { 'ộ', 'o' },
                { 'ơ', 'o' }, { 'ờ', 'o' }, { 'ớ', 'o' }, { 'ở', 'o' }, { 'ỡ', 'o' }, { 'ợ', 'o' },
                { 'ù', 'u' }, { 'ú', 'u' }, { 'ủ', 'u' }, { 'ũ', 'u' }, { 'ụ', 'u' },
                { 'ư', 'u' }, { 'ừ', 'u' }, { 'ứ', 'u' }, { 'ử', 'u' }, { 'ữ', 'u' }, { 'ự', 'u' },
                { 'ỳ', 'y' }, { 'ý', 'y' }, { 'ỷ', 'y' }, { 'ỹ', 'y' }, { 'ỵ', 'y' }
            };

            var result = new System.Text.StringBuilder();
            foreach (var c in text)
            {
                result.Append(accents.ContainsKey(c) ? accents[c] : c);
            }

            return result.ToString();
        }

        /// <summary>
        /// Parse boolean value (0/1, true/false, yes/no)
        /// </summary>
        public static bool ParseBool(string value, bool defaultValue = false)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            value = value.Trim().ToLower();

            if (value == "1" || value == "true" || value == "yes" || value == "có")
                return true;

            if (value == "0" || value == "false" || value == "no" || value == "không")
                return false;

            return defaultValue;
        }

        /// <summary>
        /// Parse integer value
        /// </summary>
        public static int ParseInt(string value, int defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            if (int.TryParse(value.Trim(), out int result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Đọc nội dung từ file Word (.docx)
        /// </summary>
        public static string ReadWordFile(string filePath)
        {
            using (var doc = WordprocessingDocument.Open(filePath, false))
            {
                var text = doc.MainDocumentPart.Document.Body.InnerText;
                return text;
            }
        }
    }
}