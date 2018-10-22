using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xaml;

namespace Furesoft.Proxy.Core
{
    public class CommandUsageProvider
    {
        public static CommandUsageProvider Instance = new CommandUsageProvider();

        private Dictionary<string, int> Count = new Dictionary<string, int>();
        private List<string> Favorites = new List<string>();

        public void Add(string name)
        {
            if(Count.ContainsKey(name))
            {
                Count[name]++;
            }
            else
            {
                Count.Add(name, 0);
            }
        }

        public string[] GetSortedCommandNames()
        {
            return Count.OrderByDescending( _ => _.Value).Select( _ => _.Key).ToArray();
        }

        

        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\proxy.cmddef";

        public void Save()
        {
            XamlServices.Save(new FileStream(path, FileMode.OpenOrCreate), Instance);
        }

        public void Load()
        {
            if (File.Exists(path))
            {
                Instance = (CommandUsageProvider)XamlServices.Load(new FileStream(path, FileMode.OpenOrCreate));
            }
        }

        public bool IsFavorite(string title)
        {
            return Favorites.Contains(title);
        }

        public void AddFavorite(string title)
        {
            Favorites.Add(title);
        }

        public void RemoveFavorite(string title)
        {
            Favorites.Remove(title);
        }
    }
}