using Furesoft.Proxy.Models;
using Furesoft.Proxy.Rpc.Interfaces;
using LiteDB;
using System.Linq;
using System.Text.RegularExpressions;

namespace Furesoft.Proxy.Service
{
    internal class FilterOperations : IFilterOperations
    {
        LiteCollection<Filter> col;

        public FilterOperations()
        {
            col = ServiceLocator.db.GetCollection<Filter>("Filter");
        }

        public void Add(Filter f)
        {
            col.Insert(f);
        }

        public Filter[] GetFilters()
        {
            return col.FindAll().ToArray();
        }

        public bool IsMatch(Filter[] fs, string src)
        {
            foreach (var f in fs)
            {
                if (f.Type == FilterType.Starts)
                {
                    return src.StartsWith(f.Pattern);
                }
                if (f.Type == FilterType.Ends)
                {
                    return src.EndsWith(f.Pattern);
                }
                if (f.Type == FilterType.Contains)
                {
                    return src.Contains(f.Pattern);
                }
                if(f.Type == FilterType.Regex)
                {
                    return Regex.IsMatch(src, f.Pattern);
                }
            }

            return false;
        }

        public bool Remove(Filter f)
        {
            if(col.FindById(f.Id) != null)
            {
                col.Delete(Query.EQ("Id", f.Id));
            }

            return false;
        }

        public void Update(Filter f)
        {
            col.Update(f);
        }
    }
}