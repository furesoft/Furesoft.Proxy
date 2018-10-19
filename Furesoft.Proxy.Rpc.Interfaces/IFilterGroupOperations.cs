namespace Furesoft.Proxy.Rpc.Interfaces
{
    public interface IFilterGroupOperations
    {
        void Reload();

        bool Add(FilterGroup f);
        bool Remove(FilterGroup f);
        bool Update(FilterGroup f);

        FilterGroup[] GetFilterGroups();
    }
}