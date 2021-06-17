using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Dal.Serveses;

namespace SiteMapGenerator.Dal.Utilities
{
    public class ServisesDi
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<StartConsole>();
            services.AddScoped<GetFromDatabase>();
            services.AddScoped<SaveDbSiteMap>();
        }
    }
}
