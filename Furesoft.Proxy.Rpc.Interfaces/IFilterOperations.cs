using Furesoft.Proxy.Models;

namespace Furesoft.Proxy.Rpc.Interfaces
{
    public interface IFilterOperations
    {
        void Add(Filter f);
        bool Remove(Filter f);
        void Update(Filter f);

        bool IsMatch(Filter[] fs, string src);

        Filter[] GetFilters();
    }
}