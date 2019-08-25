using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.UrlModel;

namespace ShortUrlForm
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            #region getting the connection string
            var builder = new ConfigurationBuilder();

            
            builder.SetBasePath(Directory.GetCurrentDirectory());

            
            builder.AddJsonFile("appsettings.json");

            
            var config = builder.Build();

            
            string connection = config.GetConnectionString("DefaultConnection");
            #endregion
            services.AddDbContext<FormContext>(options => 
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            // services.AddDbContext<FormContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("DataLayer")));



            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(options =>
            {
                //the maximum errors on model
                options.MaxModelValidationErrors = 10;

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                //path for our short links
                routes.MapRoute("path", "/{path}",
                    defaults: new { controller = "ShortUrl", action = "Path" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
