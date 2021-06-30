using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Web.Facade;

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
            services.AddScoped<HtmlParser>();
            services.AddScoped<SiteParser>();
            services.AddScoped<ParserFacade>();
            services.AddScoped<DateBaseFacade>();
        }
    }
}
