using GraphiQl;
using GraphQL;
using GraphQL.Types;
using HabrPostApi.GraphQl.Query;
using HabrPostApi.GraphQl.Schemas;
using HabrPostApi.GraphQl.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HabrPostApi
{
    internal static class GraphQlExtensions
    {
        internal static IServiceCollection AddHabrGraphQl(
            this IServiceCollection self)
        {
            self.AddTypes()
                .AddSchema()
                .AddScoped<IDependencyResolver>(
                    s => new FuncDependencyResolver(s.GetRequiredService))
                .AddScoped<IDocumentExecuter, DocumentExecuter>();

            return self;
        }

        internal static IApplicationBuilder UseHabrGraphQl(
            this IApplicationBuilder self)
            => self.UseGraphiQl("/graphiql");

        private static IServiceCollection AddSchema(
            this IServiceCollection self)
        {
            self.AddScoped<HabrQuery>()
                .AddScoped<ISchema, HabrSchema>();

            return self;
        }

        private static IServiceCollection AddTypes(
            this IServiceCollection self)
        {
            self.AddScoped<HabrHubType>()
                .AddScoped<HabrPostDetailsType>()
                .AddScoped<HabrPostType>();

            return self;
        }
    }
}