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
                    CommandUsageProvider.Add(att.Name);
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SearchableCommandAttribute : Attribute
    {
        public string Name { get; set; }

        public SearchableCommandAttribute(string name)
        {
            Name = name;
        }
    }
}