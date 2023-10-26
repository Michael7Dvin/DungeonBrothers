using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Logger
{
    public class LogWriter : ILogWriter
    {
        private string _folderPath;
        private string _filePath;

        private const string DateFormat = "yyyy-MM-dd";
        private const string LogTimeFormat = "{0:dd/MM/yyyy HH:mm:ss:ffff} [{1}]: {2}\r";
        
        public void Create()
        {
            _folderPath = $"{Application.persistentDataPath}/Logs";
            
            if (!Directory.Exists(_folderPath)) Directory.CreateDirectory(_folderPath);
            
            ManagePath();
        }

        public void Write(LogMessage logMessage)
        {
            string messageToWrite = string.Format(LogTimeFormat, logMessage.Time, logMessage.Type, logMessage.Message);

            using FileStream fileStream = File.Open(_filePath, FileMode.Append, FileAccess.Write, FileShare.Read);
            byte[] bytes = Encoding.UTF8.GetBytes(messageToWrite);
                
            fileStream.Write(bytes, 0, bytes.Length);
        }

        private void ManagePath() => _filePath = $"{_folderPath}/{DateTime.UtcNow.ToString(DateFormat)}.log";
    }
}