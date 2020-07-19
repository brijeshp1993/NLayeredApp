using log4net;

namespace LoggerPOC.Logger
{
    // Let log4net know that it can look for configuration in the default application config file
    // [assembly: log4net.Config.XmlConfigurator(Watch=true)]
    public class ServiceLogger
    {

        public ServiceLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static readonly ILog Logger = LogManager.GetLogger("LoggerPOC");

        public static void LogDebug(string value)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(value);
            }
        }

        public static void LogInfo(string value)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info(value);
            }
        }

        public static void LogWarn(string value)
        {
            if (Logger.IsWarnEnabled)
            {
                Logger.Warn(value);
            }
        }

        public static void LogError(string value)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(value);
            }
        }

        public static void LogFatal(string value)
        {
            if (Logger.IsFatalEnabled)
            {
                Logger.Fatal(value);
            }
        }

    }
}