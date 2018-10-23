﻿using System;
using System.Runtime.InteropServices;

namespace Furesoft.Proxy.Service
{
    internal static class Program
    {
        private static ProxyService proxy;

        private static void Main()
        {
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);

            proxy = new ProxyService();
            proxy.Start();

            Console.ReadLine();
        }

        private static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
#if Release
                System.Diagnostics.Process.Start(Application.ExecutablePath);
#endif
                Environment.Exit(0);
                proxy.Stop();
            }
            return false;
        }

        private static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected

        // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
    }
}