using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace eShopLegacyMVC
{
    public class WebApiConfig
    {
        public static void Register(IEndpointRouteBuilder endpoints)
        {
            // Web API routes
            endpoints.MapControllerRoute(
                name: "DefaultApi",
                pattern: "api/{controller}/{id?}"
            );
        }
    }
}