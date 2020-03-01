using GraphQL.Types;
using HabrPostApi.Models;
using System.Collections.Generic;

namespace HabrPostApi.GraphQl.Types
{
    public class HabrPostType : ObjectGraphType<HabrPost>
    {
        private static readonly string _hubs;
        private static readonly string _details;

        static HabrPostType()
        {
            _hubs = "hubs";
            _details = "details";
        }

        public HabrPostType() 
            => InitializeFields();

        private void InitializeFields()
        {
            Field(post => post.Title);
            Field(post => post.Link);
            Field<ListGraphType<HabrHubType>>(
                _hubs,
                resolve: HubsResolve);
            Field<HabrPostDetailsType>(
                _details,
                resolve: DetailsResolve);
        }

        private HabrPostDetails DetailsResolve(
            ResolveFieldContext<HabrPost> context)
            => context.Source.Details;

        private List<HabrHub> HubsResolve(
            ResolveFieldContext<HabrPost> context) 
            => context.Source.Hubs;
    }
}