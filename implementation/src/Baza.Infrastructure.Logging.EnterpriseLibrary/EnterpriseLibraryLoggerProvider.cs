using Microsoft.Framework.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Baza.Infrastructure.Logging.EnterpriseLibrary
{
    public class EnterpriseLibraryLoggerProvider : ILoggerProvider
    {
        LoggingConfiguration m_Configuration;
        string m_LoggerPath;
        List<IDisposable> m_DisposableObjects = new List<IDisposable>();

        public EnterpriseLibraryLoggerProvider()
        {

        }

        public EnterpriseLibraryLoggerProvider(string loggerPath)
        {
            Requires.NotNullOrEmpty(loggerPath, nameof(loggerPath));
            m_LoggerPath = loggerPath;
        }

        public EnterpriseLibraryLoggerProvider(LoggingConfiguration config)
        {
            Requires.NotNull(config, "config");
            m_Configuration = config;
        }

        public ILogger CreateLogger(string name)
        {
            var logger = new EnterpriseLibraryLogger(CreateLoggingConfiguration(name));
            m_DisposableObjects.Add(logger);
            return logger;
        }

        LoggingConfiguration CreateLoggingConfiguration(string name)
        {
            if (m_Configuration != null)
                return m_Configuration;

            return LoggingConfigurationFactory.Create(name, m_LoggerPath);
        }

        public void Dispose()
        {
            foreach (var disposable in m_DisposableObjects)
            {
                disposable.Dispose();
            }
        }

        public static EnterpriseLibraryLoggerProvider Create(string appName)
        {
            Requires.NotNull(appName, nameof(appName));
            var filePath = Environment.GetEnvironmentVariable("Baza:Logging:FilePath", EnvironmentVariableTarget.Machine);
            if (string.IsNullOrEmpty(filePath))
                return new EnterpriseLibraryLoggerProvider();
            return new EnterpriseLibraryLoggerProvider(Path.Combine(filePath, appName));
        }
    }
}
