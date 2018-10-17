namespace Furesoft.Proxy.Models
{
    public class Filter : BaseModel
    {
        public string Pattern { get; set; }
        public FilterType Type { get; set; }
    }

    public enum FilterType
    {
        Regex,
        Contains,
        Starts,
        Ends
    }
}