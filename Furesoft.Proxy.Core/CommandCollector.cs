using Furesoft.Proxy.Core.Attributes;
using System;
using System.Reflection;
using System.Windows.Input;

namespace Furesoft.Proxy.Core
{
    public static class CommandCollector
    {
        public static void Collect(Assembly a)
        {
            var types = a.GetTypes();

            foreach (var t in types)
            {
                var att = t.GetCustomAttribute<SearchableCommandAttribute>();

                if (att != null)
                {
                    var instance = (ICommand)Activator.CreateInstance(t);

                    SearchableCommandRepository.Instance.Add(att.Name, instance);
                    CommandUsageProvider.Instance.Add(att.Name);
                }
            }
        }
    }
}