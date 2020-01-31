using GraphQL;
using GraphQL.Types;

namespace Furesoft.Proxy
{
    public class QueryDefinition : ObjectGraphType
    {
        public QueryDefinition()
        {
            Field<StringGraphType>("hello", resolve: _ => "hello world");
        }
    }
}