using GraphQL.Types;

namespace Furesoft.Proxy
{
    public class MutationDefinition : ObjectGraphType
    {
        public MutationDefinition()
        {
            Field<StringGraphType>(
      "addContent",
      arguments: new QueryArguments(
        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
      ),
      resolve: context =>
      {
          var human = context.GetArgument<string>("name");
          return human.ToUpper();
      });
        }
    }
}