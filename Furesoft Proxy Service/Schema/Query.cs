using GraphQL;

namespace Furesoft.Proxy
{
    public class QueryDefinition
    {
        [GraphQLMetadata("hello")]
        public string GetHello()
        {
            return "Hello Query class";
        }
    }
}