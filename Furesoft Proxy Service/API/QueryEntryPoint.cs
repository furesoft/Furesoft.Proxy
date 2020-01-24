using Furesoft.Proxy.Properties;
using Furesoft.Signals.Attributes;
using GraphQL;
using GraphQL.Types;

namespace Furesoft.Proxy.API
{
    [Shared]
    public class QueryEntryPoint
    {
        [SharedFunction(0x6A3B)]
        public static string Execute(string query)
        {
            var schema = Schema.For(Resources.QueryDefinition);

            var json = schema.ExecuteAsync(_ =>
            {
                _.Query = query;
            });

            return json.Result;
        }

        [SharedFunction(0x6A3A)]
        public static string GetQueryDefinition()
        {
            return Resources.QueryDefinition;
        }
    }
}