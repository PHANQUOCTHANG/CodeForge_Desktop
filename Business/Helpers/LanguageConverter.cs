using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeForge_Desktop.Business.Helpers
{
    public static class LanguageConverter
    {
        /// <summary>
        /// Chuyển đổi kiểu dữ liệu từ định dạng chung sang kiểu dữ liệu phù hợp cho từng ngôn ngữ
        /// </summary>
        public static string ConvertParameterType(string parameterDefinition, string language)
        {
            if (string.IsNullOrWhiteSpace(parameterDefinition))
                return "";

            switch (language.ToLower())
            {
                case "c++":
                    return ConvertToCppType(parameterDefinition);
                case "python":
                    return ConvertToPythonType(parameterDefinition);
                case "javascript":
                    return ConvertToJavaScriptType(parameterDefinition);
                default:
                    return parameterDefinition;
            }
        }

        /// <summary>
        /// Chuyển đổi return type cho từng ngôn ngữ
        /// </summary>
        public static string ConvertReturnType(string returnType, string language)
        {
            if (string.IsNullOrWhiteSpace(returnType))
                return language.ToLower() == "python" ? "" : "void";

            switch (language.ToLower())
            {
                case "c++":
                    return ConvertToCppType(returnType);
                case "python":
                    return ""; // Python không cần khai báo return type
                case "javascript":
                    return ""; // JavaScript không cần khai báo return type
                default:
                    return returnType;
            }
        }

        /// <summary>
        /// Chuyển đổi kiểu sang C++
        /// </summary>
        private static string ConvertToCppType(string type)
        {
            type = type.Trim().ToLower();

            // Ánh xạ kiểu dữ liệu
            var typeMap = new Dictionary<string, string>
            {
                // Integer types
                { "int", "int" },
                { "integer", "int" },
                { "number", "int" },
                { "long", "long long" },
                { "longlong", "long long" },
                
                // Float types
                { "float", "float" },
                { "double", "double" },
                { "decimal", "double" },
                
                // String type
                { "string", "string" },
                { "char*", "string" },
                { "text", "string" },
                
                // Boolean type
                { "bool", "bool" },
                { "boolean", "bool" },
                
                // Array types
                { "int[]", "vector<int>" },
                { "integer[]", "vector<int>" },
                { "string[]", "vector<string>" },
                { "double[]", "vector<double>" },
                
                // List types
                { "list<int>", "vector<int>" },
                { "list<string>", "vector<string>" },
            };

            return typeMap.ContainsKey(type) ? typeMap[type] : type;
        }

        /// <summary>
        /// Chuyển đổi kiểu sang Python
        /// </summary>
        private static string ConvertToPythonType(string type)
        {
            // Python không cần type hints trong hàm template, nhưng có thể comment
            type = type.Trim().ToLower();

            var typeMap = new Dictionary<string, string>
            {
                { "int", "int" },
                { "integer", "int" },
                { "long", "int" },
                { "longlong", "int" },
                { "float", "float" },
                { "double", "float" },
                { "decimal", "float" },
                { "string", "str" },
                { "char*", "str" },
                { "text", "str" },
                { "bool", "bool" },
                { "boolean", "bool" },
                { "int[]", "list" },
                { "integer[]", "list" },
                { "string[]", "list" },
                { "double[]", "list" },
                { "list<int>", "list" },
                { "list<string>", "list" },
            };

            return typeMap.ContainsKey(type) ? typeMap[type] : type;
        }

        /// <summary>
        /// Chuyển đổi kiểu sang JavaScript
        /// </summary>
        private static string ConvertToJavaScriptType(string type)
        {
            // JavaScript không có type hints trong runtime, nhưng có thể comment
            type = type.Trim().ToLower();

            var typeMap = new Dictionary<string, string>
            {
                { "int", "number" },
                { "integer", "number" },
                { "long", "number" },
                { "longlong", "number" },
                { "float", "number" },
                { "double", "number" },
                { "decimal", "number" },
                { "string", "string" },
                { "char*", "string" },
                { "text", "string" },
                { "bool", "boolean" },
                { "boolean", "boolean" },
                { "int[]", "array" },
                { "integer[]", "array" },
                { "string[]", "array" },
                { "double[]", "array" },
                { "list<int>", "array" },
                { "list<string>", "array" },
            };

            return typeMap.ContainsKey(type) ? typeMap[type] : type;
        }

        /// <summary>
        /// Phân tích parameters và chuyển đổi kiểu
        /// </summary>
        public static string ParseAndConvertParameters(string parameters, string language)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return "";

            var parts = parameters.Split(',');
            var convertedParts = new List<string>();

            foreach (var part in parts)
            {
                var trimmed = part.Trim();
                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                convertedParts.Add(ConvertParameter(trimmed, language));
            }

            return string.Join(", ", convertedParts);
        }

        /// <summary>
        /// Chuyển đổi một parameter đơn lẻ
        /// </summary>
        private static string ConvertParameter(string parameter, string language)
        {
            // Tách kiểu và tên biến (VD: "int nums" -> kiểu: "int", tên: "nums")
            var parts = parameter.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length < 2)
                return parameter;

            string type = string.Join(" ", parts.Take(parts.Length - 1));
            string varName = parts.Last();

            string convertedType = ConvertParameterType(type, language);

            switch (language.ToLower())
            {
                case "python":
                    // Python: không có type hints bắt buộc
                    return varName;

                case "javascript":
                    // JavaScript: không có type hints bắt buộc
                    return varName;

                case "c++":
                default:
                    // C++: cần type
                    return $"{convertedType} {varName}";
            }
        }

        /// <summary>
        /// Tạo sample test data dựa trên type
        /// </summary>
        public static string GenerateSampleValue(string type, string language)
        {
            type = type.Trim().ToLower();

            if (type.Contains("int") || type.Contains("integer") || type.Contains("long") || type.Contains("number"))
                return "0";
            else if (type.Contains("float") || type.Contains("double") || type.Contains("decimal"))
                return "0.0";
            else if (type.Contains("string") || type.Contains("char"))
                return "\"\"";
            else if (type.Contains("bool"))
                return "false";
            else if (type.Contains("[]") || type.Contains("list"))
                return language.ToLower() == "python" ? "[]" : "[]";
            else
                return "null";
        }
    }
}