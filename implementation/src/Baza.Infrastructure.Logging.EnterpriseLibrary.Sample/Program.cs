using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Baza.Infrastructure.Logging.EnterpriseLibrary.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var appName = "Sample";
            var factory = new LoggerFactory();
            factory.AddFile();
            var logger = factory.CreateLogger(appName);
            logger.LogInformation("Application Start!");
            logger.LogInformation("Hello Boy!");
            try
            {
                try
                {
                    int.Parse("a");
                }
                catch (Exception ex)
                {
                    throw new Exception("解析数字时发生错误", ex);
                }
            }
            catch (Exception ex2)
            {
                logger.LogError("这里是写入错误测试消息", ex2);
            }
            logger.LogInformation("Application End!");
            Thread.Sleep(1000);
        }
    }
}
