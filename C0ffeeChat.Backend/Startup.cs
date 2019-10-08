using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VueCliMiddleware;

namespace C0ffeChat.Backend
{
    public class Startup
    {
        // private readonly string _projectPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, "C0ffeeChat.Frontend");

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            // In production, the Vue files will be served from this directory
            // services.AddSpaStaticFiles(configuration => { configuration.RootPath = Path.Combine(_projectPath, "dist"); });
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "../C0ffeeChat.Frontend/dist"; });
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../C0ffeeChat.Frontend";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli("serve", 8080);
                }
            });
        }
    }
}
