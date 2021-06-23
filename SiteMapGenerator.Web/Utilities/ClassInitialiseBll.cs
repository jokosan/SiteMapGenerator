using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Bll.BusinessLogic;

namespace SiteMapGenerator.Web.Utilities
{
    public class ClassInitialiseBll
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<LoadingSiteMap>();
            services.AddScoped<LinkValidator>();
            services.AddScoped<LoadingPageUrls>();
            services.AddScoped<WebRequestServeses>();
            services.AddScoped<Parser>();
        }
    }
}
