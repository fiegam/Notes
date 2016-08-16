using System;

namespace Notes.Core.Infrastructure
{
    public interface ILogger
    {
        void Debug(string message);

        void Info(string message);

        void Trace(string message);

        void Error(string message);

        void Error(string message, Exception ex);

        void Fatal(string message);

        void Fatal(string message, Exception ex);
    }
}