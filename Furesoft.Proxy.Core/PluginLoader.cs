using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Furesoft.Proxy.Core
{
    public class PluginLoader
    {
        [ImportMany(typeof(IPlugin))]
        public IPlugin[] Plugins;

        public static PluginLoader Instance = new PluginLoader();

        public static void Load()
        {
            var catalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(Instance);
        }

        public static void Init(IServiceProvider provider)
        {
            foreach (var p in Instance.Plugins)
            {
                p.Init(provider);
            }
        }

        public static object CallEvent(string name, object args = null)
        {
            object result = null;
            foreach (var p in Instance.Plugins)
            {
                result = p.OnEvent(name, args);
            }
            return result;
        }
    }
}