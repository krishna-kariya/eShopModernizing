using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace eShopLegacyMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Catalog}/{action=Index}/{id?}");
            });
        }
    }
}