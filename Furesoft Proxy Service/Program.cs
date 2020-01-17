using System;
using Topshelf;

namespace Furesoft.Proxy
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