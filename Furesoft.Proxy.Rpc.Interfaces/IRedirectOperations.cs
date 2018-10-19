using Furesoft.Proxy.Models;

namespace Furesoft.Proxy.Rpc.Interfaces
{
    public interface IRedirectOperations
    {
        void Reload();

        bool Add(RedirectFilter f);
        bool Remove(RedirectFilter f);
        bool Update(RedirectFilter f);

        RedirectFilter[] GetRedirects();
    }
}