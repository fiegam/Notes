using System.Diagnostics;

namespace Notes.Core.Infrastructure
{
    public static class LogProvider
    {
        public static ILogger GetLogger()
        {
            StackFrame frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;

            return new NlogLogger(NLog.LogManager.GetLogger(type.FullName));
        }
    }
}