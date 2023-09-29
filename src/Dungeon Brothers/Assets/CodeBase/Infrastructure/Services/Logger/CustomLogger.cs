using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Logger
{
    public class CustomLogger : ICustomLogger
    {
        private readonly ILogWriter _logWriter;

        public CustomLogger(ILogWriter logWriter)
        {
            _logWriter = logWriter;
            
            _logWriter.Create();
        }

        public void Log(object message)
        {
            _logWriter.Write(new LogMessage(LogType.Log, message.ToString()));
            
            Debug.Log($"{message}");
        }
        

        public void LogWarning(object message) 
        {
            _logWriter.Write(new LogMessage(LogType.Warning, message.ToString()));
            
            Debug.LogWarning($"{message}");
        }
        

        public void LogError(Exception exception) 
        {
            _logWriter.Write(new LogMessage(LogType.Exception, exception.Message));
            
            throw exception;
        }
    }
}