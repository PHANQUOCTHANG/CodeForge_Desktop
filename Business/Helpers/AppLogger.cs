using System;
using System.IO;
using System.Diagnostics;

namespace CodeForge_Desktop.Business.Helpers
{
    public static class AppLogger
    {
        private static readonly string _logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CodeForge");
        private static readonly string _logFile = Path.Combine(_logDir, "app.log");
        private static readonly object _lock = new object();

        private static void EnsureDirectory()
        {
            try
            {
                if (!Directory.Exists(_logDir))
                    Directory.CreateDirectory(_logDir);
            }
            catch { /* avoid throwing from logger */ }
        }

        public static void LogInfo(string message, string context = null)
        {
            Write("INFO", message, context);
        }

        public static void LogError(string message, string context = null)
        {
            Write("ERROR", message, context);
        }

        public static void LogException(Exception ex, string context = null)
        {
            if (ex == null) return;
            Write("EXCEPTION", $"{ex.GetType().FullName}: {ex.Message}\n{ex.StackTrace}", context);
        }

        private static void Write(string level, string message, string context)
        {
            try
            {
                EnsureDirectory();
                var line = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} [{level}]{(string.IsNullOrEmpty(context) ? "" : $" [{context}]")} {message}";
                lock (_lock)
                {
                    File.AppendAllText(_logFile, line + Environment.NewLine);
                }
                Trace.WriteLine(line);
            }
            catch
            {
                // Swallow to avoid logging failures impacting app flow
            }
        }
    }
}