using Furesoft.Proxy.Properties;
using Furesoft.Signals.Attributes;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Furesoft.Proxy.API
{
    [Shared]
    public class QueryEntryPoint
    {
        static QueryEntryPoint()
        {
            schema = new Schema { Query = new QueryDefinition(), Mutation = new MutationDefinition() };
        }

        [SharedFunction(0x6A3B)]
        public static string Execute(string query)
        {
            var json = schema.ExecuteAsync(_ =>
            {
                _.Query = query;
            });

            return json.Result;
        }

        [SharedFunction(0x6A3A)]
        public static string GetQueryDefinition()
        {
            return new SchemaPrinter(schema).Print();
        }

        private static Schema schema;
    }
}