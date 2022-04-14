using System;
using System.Runtime.InteropServices;
using Microsoft.Owin.Hosting;
using WalletManager.Api.Server.Applibs;

namespace WalletManager.Api.Server
{
    internal class Program
    {
        public delegate bool ConsoleCtrlDelegate(CtrlTypes ctrlType);

        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        private const int STD_INPUT_HANDLE = -10;
        private const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        private const uint ENABLE_INSERT_MODE = 0x0020;

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int hConsoleHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint mode);

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);


        public static void Main(string[] args)
        {
            using (WebApp.Start(ConfigHelper.ServiceUrl))
            {
                //// 監聽APP關閉事件
                SetConsoleCtrlHandler(t =>
                    {
                        WalletManagerProcess.ProcessStop();

                        return false;
                    },
                    true);
                DisableQuickEditMode();

                WalletManagerProcess.ProcessStart();

                while (Console.ReadLine().ToLower() != "exit")
                {
                }

                WalletManagerProcess.ProcessStop();
            }
        }

        /// <summary>
        ///     關閉快速編輯模式、插入模式
        /// </summary>
        public static void DisableQuickEditMode()
        {
            var hStdin = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            GetConsoleMode(hStdin, out mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE; //移除快速編輯模式
            mode &= ~ENABLE_INSERT_MODE; //移除插入模式
            SetConsoleMode(hStdin, mode);
        }
    }
}