using HabrPostApi.DataLoaders;
using HabrPostApi.Parsers;
using HabrPostApi.ParserWorkers;
using HabrPostApi.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace HabrPostApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCors(
            this IServiceCollection services,
            string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IServiceCollection AddHabrSwagger(
            this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {
                options
                    .SwaggerDoc()
                    .AddXmlComments();
            });

            return services;
        }

        public static IServiceCollection AddHabr(
            this IServiceCollection services)
        {
            services
                .AddScoped<IPageParserSettings, HabrParserSettings>()
                .AddScoped<IAsyncHabrDataLoader, HtmlDataLoader>()
                .AddScoped<IHabrParser, HabrParser>()
                .AddScoped<IAsyncHabrParserWorker, HabrParserWorker>();

            return services;
        }
    }
}