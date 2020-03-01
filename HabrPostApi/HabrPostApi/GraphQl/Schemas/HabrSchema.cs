using GraphQL;
using GraphQL.Types;
using HabrPostApi.GraphQl.Query;

namespace HabrPostApi.GraphQl.Schemas
{
    public class HabrSchema : Schema
    {
        public HabrSchema(IDependencyResolver resolver) : base(resolver) 
            => Query = resolver.Resolve<HabrQuery>();
    }
}