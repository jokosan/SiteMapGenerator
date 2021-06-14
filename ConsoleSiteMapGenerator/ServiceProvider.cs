using ConsoleSiteMapGenerator.Infrastructure;
using ConsoleSiteMapGenerator.Infrastructure.Contract;
using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Infrastructure.DependencyInjection;

namespace ConsoleSiteMapGenerator
{
    public class ServiceProvider
    {
        public static void RegisterServices()
        {
            IServiceCollection service = new ServiceCollection();
            service.AddScoped<IUserInteraction, UserInteraction>();
            service.AddScoped<IGeneratingSitemap, LoadingSiteAddressesConsole>();

            DependencyResolverBusinessLogic.Initialize(service);
        }
    }
}
