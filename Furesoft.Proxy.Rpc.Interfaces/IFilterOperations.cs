using Furesoft.Proxy.Models;
using System.Threading.Tasks;

namespace Furesoft.Proxy.Rpc.Interfaces
{
    public interface IFilterOperations
    {
        bool Add(Filter f);
        bool Remove(Filter f);
        bool Update(Filter f);

        bool IsMatch(Filter[] fs, string src);

        FilterCollection GetFilters();

        bool this[bool b] { get; }
    }
}