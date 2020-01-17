using System;
using Topshelf;

namespace Furesoft_Proxy_Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ProxyService>();
                x.EnableServiceRecovery(r => r.RestartService(TimeSpan.FromSeconds(10)));
                x.SetServiceName("Furesoft Proxy");
                x.SetDescription("Proxy Service");
                x.StartAutomatically();
            });
        }
    }
}