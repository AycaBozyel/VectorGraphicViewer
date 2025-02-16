using System.IO;

namespace VectorGraphicViewer.Logging
{
    public class LogService : ILogService
    {
        private string logFilePath = "application_log.txt"; 

        public void Log(string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {ex.Message}" + Environment.NewLine);
            }
        }

        public void LogError(string message, Exception ex)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {message} - Exception: {ex.Message}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
