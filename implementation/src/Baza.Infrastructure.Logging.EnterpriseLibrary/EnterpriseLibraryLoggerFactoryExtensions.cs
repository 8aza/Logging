
using Baza.Infrastructure.Logging.EnterpriseLibrary;

namespace Microsoft.Extensions.Logging
{
    public static class EnterpriseLibraryLoggerFactoryExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory)
        {
            Requires.NotNull(factory, nameof(factory));
            factory.AddProvider(new EnterpriseLibraryLoggerProvider());
            return factory;
        }
    }
}
