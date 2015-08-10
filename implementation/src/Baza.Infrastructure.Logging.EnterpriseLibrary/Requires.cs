using System;

namespace Baza.Infrastructure.Logging.EnterpriseLibrary
{
    internal static class Requires
    {
        public static void NotNull<T>(T value, string parameterName)
            where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }
    }
}
