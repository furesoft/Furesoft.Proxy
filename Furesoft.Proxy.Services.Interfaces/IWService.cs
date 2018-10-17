using Furesoft.Proxy.Core;

namespace Furesoft.Proxy.Services.Interfaces
{
    public interface IWService : IService
    {
        bool IsRunning();

        void Start();

        void Stop();
    }
}