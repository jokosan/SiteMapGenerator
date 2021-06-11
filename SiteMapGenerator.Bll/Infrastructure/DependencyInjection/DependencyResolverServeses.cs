using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Bll.Services;
using SiteMapGenerator.Bll.Services.Contract;

namespace SiteMapGenerator.Bll.Infrastructure.DependencyInjection
{
    public class DependencyResolverServeses
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IArchiveOfRequest, ArchiveOfRequestServeses>();
            services.AddScoped<IPageInfo, PageInfoServeses>();
            services.AddScoped<IUrlSiteMap, UrlSiteMapServeses>();
        }
    }
}
