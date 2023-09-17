namespace CodeBase.Infrastructure.Services.Logging
{
    public interface ICustomLogger
    {
        void Log(object message);
        void LogWarning(object message);
        void LogError(object message);
    }
}