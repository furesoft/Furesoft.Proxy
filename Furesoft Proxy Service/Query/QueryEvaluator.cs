using Furesoft.Proxy.Language;

namespace Furesoft.Proxy.Query
{
    public static class QueryEvaluator
    {
        public static QueryEvaluationResult ParseQuery(string src)
        {
            var parser = new BlockQueryGrammar();
            var ast = parser.Parse(src);

            return null;
        }
    }
}