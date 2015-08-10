using Microsoft.Framework.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Baza.Infrastructure.Logging.EnterpriseLibrary
{
    public class EnterpriseLibraryLoggerProvider : ILoggerProvider
    {
        LoggingConfiguration m_Configuration;
        public EnterpriseLibraryLoggerProvider()
        { }

        public EnterpriseLibraryLoggerProvider(LoggingConfiguration config)
        {
            Requires.NotNull(config, "config");
            m_Configuration = config;
        }

        public ILogger CreateLogger(string name)
        {
            return new EnterpriseLibraryLogger(m_Configuration ?? LoggingConfigurationFactory.Create(name));
        }
    }
}
