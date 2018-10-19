using Furesoft.Proxy.Models;

namespace Furesoft.Proxy.Rpc.Interfaces
{
    public interface IFilterOperations
    {
        void Reload();

        bool Add(Filter f);
        bool Remove(Filter f);
        bool Update(Filter f);

        Filter[] GetFilters();
    }
}