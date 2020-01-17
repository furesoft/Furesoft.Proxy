using Titanium.Web.Proxy.Http;

namespace Furesoft.Proxy.Query
{
    public class QueryEvaluationResult
    {
        public bool IsBlocked { get; set; }

        public QueryType Type { get; set; }

        public enum QueryType { Message, Redirect, Content }

        public void DoBlock(HttpWebClient client)
        {
        }
    }
}