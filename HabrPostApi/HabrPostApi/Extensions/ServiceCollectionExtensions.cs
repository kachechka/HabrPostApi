using HabrPostApi.DataLoaders;
using HabrPostApi.Parsers;
using HabrPostApi.ParserWorkers;
using HabrPostApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HabrPostApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly string _policyName;

        static ServiceCollectionExtensions()
            => _policyName = "AllowAll";

        public static IServiceCollection AddHabrCors(
            this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_policyName, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseHabrCors(
            this IApplicationBuilder self)
            => self.UseCors(_policyName);

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

        public static IServiceCollection AddSettings<TService, TImplemnt>(
            this IServiceCollection self,
            IConfiguration configuration)
            where TService : class
            where TImplemnt : class, TService, new()
        {
            var sectionName = typeof(TImplemnt).Name;
            var section = configuration.GetSection(sectionName);

            self.Configure<TImplemnt>(section);

            self.AddScoped<TService>(sp =>
                sp.GetRequiredService<IOptions<TImplemnt>>().Value);

            return self;
        }

        public static IServiceCollection AddHabr(
            this IServiceCollection services)
        {
            services
                .AddScoped<IPageParserSettings, HabrParserSettings>()
                .AddScoped<IHabrDataLoader, HtmlDataLoader>()
                .AddScoped<IHabrParser, HabrParser>()
                .AddScoped<IHabrParserWorker, HabrParserWorker>();

            return services;
        }
    }
}