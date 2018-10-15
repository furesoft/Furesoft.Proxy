using Furesoft.Proxy.Core;

namespace Furesoft.Proxy.Services.Interfaces
{
    public interface IAuthService : IService
    {
        void Login(string username, string password);
        void Logout();
    }
}