using UnityEngine;

namespace CodeBase.Infrastructure.Services.Logging
{
    public class CustomLogger : ICustomLogger
    {
        public void Log(object message) => 
            Debug.Log(message);

        public void LogWarning(object message) => 
            Debug.LogWarning(message);

        public void LogError(object message) => 
            Debug.LogError(message);
    }
}