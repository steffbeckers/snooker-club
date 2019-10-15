using GraphQL;
using GraphQL.Types;

namespace SC.API.GraphQL
{
    public class SCSchema : Schema
    {
        public SCSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<SCQuery>();
            Mutation = resolver.Resolve<SCMutation>();
        }
    }
}
