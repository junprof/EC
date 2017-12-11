using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.Common
{
    public class Log
    {
        private static readonly ILog DebugLogger = LogManager.GetLogger("DebugLogger");
        private static readonly ILog ErrorLogger = LogManager.GetLogger("ErrorLogger");
        private static readonly ILog FatalLogger = LogManager.GetLogger("FatalLogger");
        private static readonly ILog InfoLogger = LogManager.GetLogger("InfoLogger");
        private static readonly ILog WarnLogger = LogManager.GetLogger("WarnLogger");
        private static readonly bool isDebugEnable = DebugLogger.IsDebugEnabled;
        private static readonly bool isErrorEnabled = ErrorLogger.IsErrorEnabled;
        private static readonly bool isFatalEnabled = FatalLogger.IsFatalEnabled;
        private static readonly bool isInfoEnabled = InfoLogger.IsInfoEnabled;
        private static readonly bool isWarnEnabled = WarnLogger.IsWarnEnabled;

        public static void Debug(string msg)
        {
            if (isDebugEnable)
            {
                DebugLogger.Debug(msg);
            }
        }

        public static void Error(string msg)
        {
            if (isErrorEnabled)
            {
                ErrorLogger.Error(msg);
            }
        }

        public static void Fatal(string msg)
        {
            if (isFatalEnabled)
            {
                FatalLogger.Fatal(msg);
            }
        }

        public static void Info(string msg)
        {
            if (isInfoEnabled)
            {
                InfoLogger.Info(msg);
            }
        }

        public static void Warn(string msg)
        {
            if (isWarnEnabled)
            {
                WarnLogger.Warn(msg);
            }
        }
    }
}