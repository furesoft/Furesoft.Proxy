using System;
using System.Collections.Generic;

namespace Furesoft.Proxy.Core
{
    public class ServiceProvider : IServiceProvider
    {
        private Dictionary<Type, object> services = new Dictionary<Type, object>();

        public ServiceProvider()
        {
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return services[serviceType];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public void AddService(object service)
        {
            services.Add(service.GetType(), service);
        }

        public T AddService<T>(T service)
        {
            if (!services.ContainsKey(typeof(T)))
            {
                services.Add(typeof(T), service);
            }

            return (T)service;
        }

        public void AddService<T>() where T : new()
        {
            services.Add(typeof(T), new T());
        }
    }
}