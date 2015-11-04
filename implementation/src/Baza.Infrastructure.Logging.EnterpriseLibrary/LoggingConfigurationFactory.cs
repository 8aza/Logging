using Microsoft.Framework.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System.Diagnostics;

namespace Baza.Infrastructure.Logging.EnterpriseLibrary
{
    class LoggingConfigurationFactory
    {
        static readonly TextFormatter _SimplyFormatter = new TextFormatter("[{severity}, {timestamp(local:F)}] {message}");

        public static LoggingConfiguration Create(string name, string filePath = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Default";
            }

            // Trace Listeners
            var listener = new RollingFlatFileTraceListener((filePath ?? "Logging") + "\\" + name + ".log",
                header: string.Empty, footer: string.Empty, formatter: _SimplyFormatter,
                rollFileExistsBehavior: RollFileExistsBehavior.Increment, rollInterval: RollInterval.Day, maxArchivedFiles: 30);

            // Build Configuration
            var config = new LoggingConfiguration();
            config.DefaultSource = LogLevel.Information.ToString();
            config.IsLoggingEnabled = true;
            //config.AddLogSource(Logger.InformationCategoryName, SourceLevels.All, autoFlush: true).AddAsynchronousTraceListener(listener);
            // Special Sources Configuration
            //config.SpecialSources.Unprocessed.AddTraceListener(listener);
            config.SpecialSources.Unprocessed.Level = SourceLevels.All;
            config.SpecialSources.Unprocessed.AutoFlush = true;
            var eventLogListener = CreateEventLogTraceListener();
            config.SpecialSources.LoggingErrorsAndWarnings.AddTraceListener(eventLogListener);
            config.SpecialSources.LoggingErrorsAndWarnings.Level = SourceLevels.All;
            config.SpecialSources.LoggingErrorsAndWarnings.AutoFlush = true;
            // All Event
            config.SpecialSources.AllEvents.AddAsynchronousTraceListener(listener);
            config.SpecialSources.AllEvents.Level = SourceLevels.All;
            config.SpecialSources.AllEvents.AutoFlush = true;
            return config;
        }

        static TraceListener CreateEventLogTraceListener()
        {
            return new FormattedEventLogTraceListener(new EventLog("Application"), _SimplyFormatter);
        }
    }
}
