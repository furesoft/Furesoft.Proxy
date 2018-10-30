using Furesoft.Proxy.Core;
using System;
using System.Windows;
using ToastNotifications.Messages;

namespace Furesoft.Proxy
{
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            NotificationManager.notifier.ShowInformation(e.Exception.Message);

            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            
            NotificationManager.Init();

            DispatcherUnhandledException += Application_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            CommandUsageProvider.Instance.Load();


            base.OnStartup(e);
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            CommandUsageProvider.Instance.Save();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            NotificationManager.notifier.ShowInformation((e.ExceptionObject as Exception).Message);
        }
    }
}