using Furesoft.Proxy.Services;
using Furesoft.Proxy.Services.Interfaces;
using System;
using System.Reflection;
using System.Windows;
using ToastNotifications.Messages;

namespace Furesoft.Proxy
{
    public partial class App : Application
    {
       
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            NotificationManager.notifier.ShowError(e.Exception.ToString());

            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //ToDo: Change to Rpc Service
            ServiceLocator.Instance.Provider.AddService<IWService>(new WService());
            NotificationManager.Init();
            DispatcherUnhandledException += Application_DispatcherUnhandledException;

            base.OnStartup(e);
        }
    }
}