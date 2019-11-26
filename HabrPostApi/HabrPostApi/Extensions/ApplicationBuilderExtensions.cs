using Microsoft.AspNetCore.Builder;

namespace HabrPostApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHabrSwagger(
            this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "Habr posts API");
                options.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}