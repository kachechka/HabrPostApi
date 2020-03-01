using System.Net;
using HabrPostApi.Extensions;
using HabrPostApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HabrPostApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHabr()
                .AddSettings<IHabrSelector, HabrSelector>(Configuration)
                .AddHabrCors()
                .AddHabrSwagger()
                .AddHabrGraphQl();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHabrCors()
                .UseHabrGraphQl();

            ConfigSecurityProtocol();

            app.UseHabrSwagger();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigSecurityProtocol()
        {
            ServicePointManager.SecurityProtocol |=
                SecurityProtocolType.Tls12
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls;
        }
    }
}