using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace WalletManager.Api.Server.Applibs
{
    public class WalletManagerProcess
    {
        private const string PROCESS_NAME = "WalletManager.Api.Server";
        private static readonly ILogger logger = LogManager.GetLogger(PROCESS_NAME);

        public static void ProcessStart()
        {
            logger.Info("Application Start");
            var container = AutofacConfig.Container;

            Task.Run(() =>
            {
                while (!SpinWait.SpinUntil(() => false, 1000))
                {
                    Console.Clear();
                    Console.WriteLine($"Current Memory Usage:{(int) (GC.GetTotalMemory(true) / 1024f)}(KB)");
                    Console.WriteLine(
                        $"Process Memory Usage:{(int) (Process.GetCurrentProcess().PrivateMemorySize64 / 1024f)}(KB)");
                    Console.WriteLine($"Handle count:{Process.GetCurrentProcess().HandleCount}");
                    Console.WriteLine($"Thread count:{Process.GetCurrentProcess().Threads.Count}");
                }
            });

            logger.Info("Application Started");
        }


        public static void ProcessStop()
        {
            logger.Info("Application Ended");
        }
    }
}