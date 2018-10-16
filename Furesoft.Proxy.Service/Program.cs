using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Furesoft.Proxy.Service
{
    static class Program
    {
        static ProxyService proxy;

        static void Main()
        {
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);

            proxy = new ProxyService();
            proxy.Start();

            Console.ReadLine();
        }

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);

                Environment.Exit(0);
                proxy.Stop();
            }
            return false;
        }

        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected

        // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

    }
}