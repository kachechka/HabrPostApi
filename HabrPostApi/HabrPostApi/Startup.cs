using System.Net;
using HabrPostApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HabrPostApi
{
    public class Startup
    {
        private readonly string _policyName;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _policyName = "AllowAll";
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHabr();

            services.AddCors(_policyName);

            services.AddHabrSwagger();

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

            app.UseCors(_policyName);

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