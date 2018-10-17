using System;

namespace Furesoft.Proxy.Core
{
    public interface IPlugin
    {
        object OnEvent(string e, object arg = null);
        void Init(IServiceProvider provider);
    }
}
