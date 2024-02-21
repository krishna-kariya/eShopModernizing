using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDistributedMemoryCache(); // Add this line  

        services.AddControllersWithViews();

        // Add session services.  
        services.AddSession(options => {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Set a short timeout for testing.  
        });
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

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseSession();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            // MVC routes  
            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Catalog}/{action=Index}/{id?}");

            // Web API routes  
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "DefaultApi",
                pattern: "api/{controller}/{id?}");
        });
    }
}
