using System;

namespace Furesoft.Proxy.Core
{
    public static class IServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider p)
            where T : IService
        {
            return (T)p.GetService(typeof(T));
        }
    }
}