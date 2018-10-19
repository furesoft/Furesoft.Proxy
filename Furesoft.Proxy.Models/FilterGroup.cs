using Furesoft.Proxy.Core;

namespace Furesoft.Proxy.Models
{
    public class FilterGroup : BaseModel
    {
        public string Name { get; set; }

        public Filter[] Filters { get; set; }
    }
}