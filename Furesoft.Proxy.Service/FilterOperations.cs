﻿using Furesoft.Proxy.Models;
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

        public bool Add(Filter f)
        {
            if (col.FindOne(Query.EQ("Name", f.Name)) != null) return false;
            else { col.Insert(f); return true; }
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
            if(col.FindOne(Query.EQ("Name", f.Name)) != null)
            {
                col.Delete(Query.EQ("Id", f.Id));
            }

            return false;
        }

        public bool Update(Filter f)
        {
            if (col.FindOne(Query.EQ("Name", f.Name)) != null) return false;
            else { col.Update(f); return true; }
        }
    }
}