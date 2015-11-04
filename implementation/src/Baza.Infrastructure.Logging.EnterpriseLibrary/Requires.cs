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

        public static void NotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(parameterName + "不能为null或空字符串");
        }
    }
}
