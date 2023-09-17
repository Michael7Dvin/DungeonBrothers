namespace CodeBase.Infrastructure.Services.Logging
{
    public interface ILogWriter
    {
        public void Create();
        public void Write(LogMessage logMessage);
    }
}