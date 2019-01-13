using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Spoonson.Common
{
    /// <summary>
    /// 日志类
    /// </summary>
    public static class Logger
    {
        private static ILog m_log { get; } = LogManager.GetLogger(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

        public static void Debug(string msg)
        {
            m_log.Debug(msg);
        }

        public static void Info(string msg)
        {
            m_log.Info(msg);
        }

        public static void Warn(string msg)
        {
            m_log.Warn(msg);
        }

        public static void Error(string msg)
        {
            m_log.Error(msg);
        }

        public static void Fatal(string msg)
        {
            m_log.Fatal(msg);
        }
    }
}
