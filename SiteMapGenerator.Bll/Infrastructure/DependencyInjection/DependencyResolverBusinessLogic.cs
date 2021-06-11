using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Services.Contract;

namespace SiteMapGenerator.Bll.Infrastructure.DependencyInjection
{
    public class DependencyResolverBusinessLogic
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<ILinkCheck, LinkCheck>();
            services.AddScoped<ILoadingPageUrls, LoadingPageUrls>();
            services.AddScoped<ILoadingSiteAddresses, LoadingSiteAddresses>();
            services.AddScoped<IOutput, Output>();
            services.AddScoped<IWebsiteLoadingSpeed, WebsiteLoadingSpeed>();
        }
    }
}
