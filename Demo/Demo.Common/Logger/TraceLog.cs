using System;

namespace Demo.Common.Logger
{
    public class TraceLog : ILogger
    {
        public void Debug(object item)
        {
            //throw new NotImplementedException();
        }

        public void Debug(string message, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void Debug(string message, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void Fatal(string message, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void LogError(string message, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void LogInfo(string message, params object[] args)
        {
            //throw new NotImplementedException();
        }

        public void LogWarning(string message, params object[] args)
        {
            //throw new NotImplementedException();
        }
    }
}
