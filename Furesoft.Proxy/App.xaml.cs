using Furesoft.Proxy.Services;
using Furesoft.Proxy.Services.Interfaces;
using System.Windows;

namespace Furesoft.Proxy
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceLocator.Instance.Provider.AddService<IWService>(new WService());
            NotificationManager.Init();

            base.OnStartup(e);
        }
    }
}