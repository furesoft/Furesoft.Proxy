using System.ServiceProcess;

namespace Furesoft.Proxy.Service
{

    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ProxyService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}