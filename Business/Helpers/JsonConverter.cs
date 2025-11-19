using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeForge_Desktop.Business.Helpers
{
    public static class JsonConverter
    {
        /// <summary>
        /// Chuyển đổi JSON string sang định dạng biến = giá trị (LeetCode style)
        /// Ví dụ: {"s":"abc","nums":[1,2,3]} -> s = "abc", nums = [1,2,3]
        /// </summary>
        public static string JsonToVariableFormat(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                return "";

            try
            {
                jsonString = jsonString.Trim();

                // Loại bỏ dấu {} ở đầu và cuối
                if (jsonString.StartsWith("{") && jsonString.EndsWith("}"))
                    jsonString = jsonString.Substring(1, jsonString.Length - 2);

                var pairs = new List<string>();
                var currentPair = new StringBuilder();
                var inQuotes = false;
                var inArray = false;
                var bracketDepth = 0;

                for (int i = 0; i < jsonString.Length; i++)
                {
                    char c = jsonString[i];

                    if (c == '"' && (i == 0 || jsonString[i - 1] != '\\'))
                    {
                        inQuotes = !inQuotes;
                        currentPair.Append(c);
                    }
                    else if (c == '[' && !inQuotes)
                    {
                        inArray = true;
                        bracketDepth++;
                        currentPair.Append(c);
                    }
                    else if (c == ']' && !inQuotes)
                    {
                        bracketDepth--;
                        if (bracketDepth == 0)
                            inArray = false;
                        currentPair.Append(c);
                    }
                    else if (c == ',' && !inQuotes && !inArray)
                    {
                        if (currentPair.Length > 0)
                        {
                            pairs.Add(currentPair.ToString().Trim());
                            currentPair.Clear();
                        }
                    }
                    else
                    {
                        currentPair.Append(c);
                    }
                }

                // Thêm cặp cuối cùng
                if (currentPair.Length > 0)
                    pairs.Add(currentPair.ToString().Trim());

                // Chuyển đổi từng cặp
                var result = new StringBuilder();
                for (int i = 0; i < pairs.Count; i++)
                {
                    string pair = pairs[i];
                    string formatted = FormatPair(pair);
                    result.Append(formatted);

                    if (i < pairs.Count - 1)
                        result.Append(Environment.NewLine);
                }

                return result.ToString();
            }
            catch
            {
                return jsonString;
            }
        }

        /// <summary>
        /// Chuyển đổi một cặp key-value
        /// "s":"abc" -> s = "abc"
        /// "nums":[1,2,3] -> nums = [1,2,3]
        /// </summary>
        private static string FormatPair(string pair)
        {
            if (string.IsNullOrWhiteSpace(pair))
                return "";

            int colonIndex = pair.IndexOf(':');
            if (colonIndex == -1)
                return pair;

            string key = pair.Substring(0, colonIndex).Trim();
            string value = pair.Substring(colonIndex + 1).Trim();

            // Loại bỏ dấu ngoặc kép ở key
            if (key.StartsWith("\"") && key.EndsWith("\""))
                key = key.Substring(1, key.Length - 2);

            // Chuyển đổi giá trị
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                // String value - giữ nguyên dấu ngoặc kép
                return $"{key} = {value}";
            }
            else if (value == "true" || value == "false" || value == "null")
            {
                // Boolean hoặc null
                return $"{key} = {value}";
            }
            else if (value.StartsWith("[") && value.EndsWith("]"))
            {
                // Array
                return $"{key} = {value}";
            }
            else
            {
                // Number
                return $"{key} = {value}";
            }
        }

        /// <summary>
        /// Chuyển đổi định dạng biến = giá trị về JSON
        /// s = "abc", nums = [1,2,3] -> {"s":"abc","nums":[1,2,3]}
        /// </summary>
        public static string VariableFormatToJson(string variableString)
        {
            if (string.IsNullOrWhiteSpace(variableString))
                return "{}";

            try
            {
                var pairs = new List<string>();
                var lines = variableString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (var line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (string.IsNullOrWhiteSpace(trimmedLine))
                        continue;

                    int eqIndex = trimmedLine.IndexOf('=');
                    if (eqIndex == -1)
                        continue;

                    string key = trimmedLine.Substring(0, eqIndex).Trim();
                    string value = trimmedLine.Substring(eqIndex + 1).Trim();

                    // Xây dựng JSON pair
                    string jsonPair = $"\"{key}\":{value}";
                    pairs.Add(jsonPair);
                }

                return "{" + string.Join(",", pairs) + "}";
            }
            catch
            {
                return "{}";
            }
        }
    }
}