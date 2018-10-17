using Furesoft.Proxy.Core;

namespace Furesoft.Proxy.Models
{
    public class RedirectFilter : BaseModel
    {
        public string OldUri { get; set; }
        public string NewUri { get; set; }
    }
}