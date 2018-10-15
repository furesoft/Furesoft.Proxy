using Furesoft.Proxy.Services.Interfaces;
using System.ServiceProcess;

namespace Furesoft.Proxy.Services
{
    public class WService : IWService
    {
        //ToDo: implement communication with service to reload database

        ServiceController sc = new ServiceController("Furesoft Proxy Service");

        public bool IsRunning()
        {
            return sc.Status == ServiceControllerStatus.Running;
        }

        public void Start()
        {
            sc.Start();
        }

        public void Stop()
        {
            sc.Stop();
        }
    }
}