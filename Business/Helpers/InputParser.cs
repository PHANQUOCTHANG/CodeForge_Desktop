using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CodeForge_Desktop.Business.Helpers
{
    /// <summary>
    /// Utility class để parse input từ format "variable=value,variable2=value" sang JSON
    /// </summary>
    public static class InputParser
    {
        /// <summary>
        /// Convert input từ format "a=5,b=10" sang JSON format {"a":5,"b":10}
        /// </summary>
        public static string ParseInputToJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "{}";

            try
            {
                var dictionary = new Dictionary<string, object>();
                string[] pairs = input.Split(',');

                if (pairs.Length == 0)
                    return "{}";

                foreach (string pair in pairs)
                {
                    string trimmedPair = pair.Trim();
                    if (string.IsNullOrEmpty(trimmedPair))
                        continue;

                    // Find the = sign (but not inside quotes or brackets)
                    int equalsIndex = FindEqualsIndex(trimmedPair);
                    if (equalsIndex < 0)
                        throw new FormatException($"Cặp '{trimmedPair}' không có dấu '=' hợp lệ.");

                    string variableName = trimmedPair.Substring(0, equalsIndex).Trim();
                    string variableValue = trimmedPair.Substring(equalsIndex + 1).Trim();

                    // Validate variable name
                    if (!Regex.IsMatch(variableName, @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
                        throw new FormatException($"Tên biến '{variableName}' không hợp lệ. Tên biến phải bắt đầu bằng chữ cái hoặc gạch dưới.");

                    if (string.IsNullOrEmpty(variableValue))
                        throw new FormatException($"Giá trị của biến '{variableName}' không được để trống.");

                    // Parse the value to appropriate type
                    object parsedValue = ParseValue(variableValue);
                    dictionary[variableName] = parsedValue;
                }

                if (dictionary.Count == 0)
                    return "{}";

                // Convert to JSON
                return JsonConvert.SerializeObject(dictionary);
            }
            catch (FormatException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FormatException($"Lỗi khi parse input: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Parse value string to appropriate type
        /// </summary>
        private static object ParseValue(string value)
        {
            value = value.Trim();

            // null
            if (value.Equals("null", StringComparison.OrdinalIgnoreCase))
                return null;

            // boolean
            if (bool.TryParse(value, out bool boolValue))
                return boolValue;

            // integer
            if (int.TryParse(value, out int intValue))
                return intValue;

            // long
            if (long.TryParse(value, out long longValue))
                return longValue;

            // double
            if (double.TryParse(value, out double doubleValue))
                return doubleValue;

            // quoted string
            if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                (value.StartsWith("'") && value.EndsWith("'")))
            {
                return value.Substring(1, value.Length - 2);
            }

            // array (keep as string)
            if (value.StartsWith("[") && value.EndsWith("]"))
                return value;

            // default: treat as string
            return value;
        }

        /// <summary>
        /// Find index of '=' not inside quotes or brackets
        /// </summary>
        private static int FindEqualsIndex(string str)
        {
            bool inDoubleQuotes = false;
            bool inSingleQuotes = false;
            int bracketDepth = 0;
            char prevChar = '\0';

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                // Handle escaped characters
                if (prevChar == '\\')
                {
                    prevChar = c;
                    continue;
                }

                // Track quotes
                if (c == '"' && !inSingleQuotes)
                    inDoubleQuotes = !inDoubleQuotes;
                else if (c == '\'' && !inDoubleQuotes)
                    inSingleQuotes = !inSingleQuotes;

                // Track brackets
                if (!inDoubleQuotes && !inSingleQuotes)
                {
                    if (c == '[')
                        bracketDepth++;
                    else if (c == ']')
                        bracketDepth--;
                    else if (c == '=' && bracketDepth == 0)
                        return i;
                }

                prevChar = c;
            }

            return -1;
        }

        /// <summary>
        /// Parse JSON back to dictionary
        /// </summary>
        public static Dictionary<string, object> ParseJsonToDict(string json)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(json))
                    return new Dictionary<string, object>();

                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json) ?? 
                       new Dictionary<string, object>();
            }
            catch
            {
                return new Dictionary<string, object>();
            }
        }
    }
}