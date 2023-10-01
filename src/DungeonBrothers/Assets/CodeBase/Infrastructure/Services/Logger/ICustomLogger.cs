using System;

namespace CodeBase.Infrastructure.Services.Logger
{
    public interface ICustomLogger
    {
        void Log(object message);
        void LogWarning(object message);
        public void LogError(Exception exception) =>
            throw exception;
    }
}