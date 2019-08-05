using gnomi.common.utility.reflection;
using gnomi.dataService.entities;
using gnomi.dataService.services;
using gnomi.repositories;
using gnomi.repositories.connection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace gnomi.dataService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            settings.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<iHumanService, humanService>();
            services.AddScoped<iHumanRepository<long, human<long>>, humanRepository<long, human<long>>>();
            services.AddScoped<iInstanceAnalyzer, instanceAnalyzer>();
            services.AddTransient<iDataConnectionFactory>(f => new sqlDataConnectionFactory(settings.dataConnectionString));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
