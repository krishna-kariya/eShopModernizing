using Autofac;
using Autofac.Extensions.DependencyInjection;
using eShopPorted.Models;
using eShopPorted.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace eShopPorted
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static DateTime StartTime { get; } = DateTime.UtcNow;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            bool useMockData = Configuration.GetValue<bool>("UseMockData");
            if (!useMockData)
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<CatalogDBContext>(options =>
                    options.UseSqlServer(connectionString)
                );
            }

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ApplicationModule(useMockData));

            ILifetimeScope container = builder.Build();
            services.AddSingleton<IServiceProvider>(new AutofacServiceProvider(container));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalog}/{action=Index}/{id?}");
            });
        }
    }
}