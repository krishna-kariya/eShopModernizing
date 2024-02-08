using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace eShopLegacyMVC
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles(); // For the wwwroot folder

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Scripts")),
                RequestPath = "/Scripts"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = "/Content"
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}