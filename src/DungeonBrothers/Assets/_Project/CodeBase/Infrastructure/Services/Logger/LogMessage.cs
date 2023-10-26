using System;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Logger
{
    public struct LogMessage
    {
        public LogType Type { get;}
        public DateTime Time { get;}
        public string Message { get;}

        public LogMessage(LogType type,
            string message)
        {
            Type = type;
            Message = message;
            Time = DateTime.UtcNow;
        }
    }
}