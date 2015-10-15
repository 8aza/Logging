using Microsoft.Framework.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;

namespace Baza.Infrastructure.Logging.EnterpriseLibrary
{
    public class EnterpriseLibraryLoggerProvider : ILoggerProvider
    {
        LoggingConfiguration m_Configuration;
        List<IDisposable> m_DisposableObjects = new List<IDisposable>();

        public EnterpriseLibraryLoggerProvider()
        { }

        public EnterpriseLibraryLoggerProvider(LoggingConfiguration config)
        {
            Requires.NotNull(config, "config");
            m_Configuration = config;
        }

        public ILogger CreateLogger(string name)
        {
            var logger = new EnterpriseLibraryLogger(m_Configuration ?? LoggingConfigurationFactory.Create(name));
            m_DisposableObjects.Add(logger);
            return logger;
        }

        public void Dispose()
        {
            foreach (var disposable in m_DisposableObjects)
            {
                disposable.Dispose();
            }
        }
    }
}
