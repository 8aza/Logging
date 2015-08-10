using Microsoft.Framework.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Diagnostics;

namespace Baza.Infrastructure.Logging.EnterpriseLibrary
{
    public class EnterpriseLibraryLogger : ILogger
    {
        LogWriter m_LogWriter;
        public EnterpriseLibraryLogger()
            : this(LoggingConfigurationFactory.Create(null))
        {
        }

        public EnterpriseLibraryLogger(LoggingConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            m_LogWriter = new LogWriter(config);
        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            var eventType = ToTraceEventType(logLevel);
            var message = string.Empty;
            if (formatter != null)
            {
                message = formatter(state, exception);
            }
            else
            {
                message = LogFormatter.Formatter(state, exception);
            }
            if (!string.IsNullOrEmpty(message))
            {
                m_LogWriter.Write(message, logLevel.ToString(), 0, eventId, eventType);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return m_LogWriter.ShouldLog(new LogEntry() { Severity = ToTraceEventType(logLevel) });
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return null;
        }

        static TraceEventType ToTraceEventType(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Verbose:
                    return TraceEventType.Verbose;
                case LogLevel.Information:
                    return TraceEventType.Information;
                case LogLevel.Warning:
                    return TraceEventType.Warning;
                case LogLevel.Error:
                    return TraceEventType.Error;
                case LogLevel.Critical:
                    return TraceEventType.Critical;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
}
