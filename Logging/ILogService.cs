namespace VectorGraphicViewer.Logging
{
    public interface ILogService
    {
        void Log(string message);
        void LogError(string message, Exception ex);
    }
}
