using System.Collections.Generic;
using System.Linq;

namespace Furesoft.Proxy.Core
{
    public class CommandUsageProvider
    {
        private static Dictionary<string, int> Count = new Dictionary<string, int>();

        public static void Add(string name)
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

        public static string[] GetSortedCommandNames()
        {
            return Count.OrderByDescending( _ => _.Value).Select( _ => _.Key).ToArray();
        }
    }
}