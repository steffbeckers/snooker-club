using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.GraphQL
{
    public class SCSchema : Schema
    {
        public SCSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<SCQuery>();
        }
    }
}
