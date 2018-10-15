using Furesoft.Proxy.Services;
using Furesoft.Proxy.Services.Interfaces;
using System.ServiceProcess;
using System.Windows;

namespace Furesoft.Proxy
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceLocator.Provider.AddService<IWService>(new WService());
            
            base.OnStartup(e);
        }
    }
}