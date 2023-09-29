namespace CodeBase.Infrastructure.Services.Logger
{
    public interface ILogWriter
    {
        public void Create();
        public void Write(LogMessage logMessage);
    }
}