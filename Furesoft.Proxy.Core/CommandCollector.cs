using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

                if(att != null)
                {
                        var instance = (ICommand)Activator.CreateInstance(t);

                        SearchableCommandRepository.Instance.Add(att.Name, instance);
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