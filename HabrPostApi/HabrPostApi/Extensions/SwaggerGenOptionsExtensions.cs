using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace HabrPostApi.Extensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static SwaggerGenOptions AddXmlComments(
            this SwaggerGenOptions options)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            var xmlFile = $"{assemblyName.Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            return options;
        }

        public static SwaggerGenOptions SwaggerDoc(
            this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info
            {
                Version = "v1",
                Title = "Habr posts api",
                Contact = new Contact
                {
                    Name = "Kachechka Artur",
                    Email = "kachechka.artur@gmail.com"
                }
            });

            return options;
        }
    }
}