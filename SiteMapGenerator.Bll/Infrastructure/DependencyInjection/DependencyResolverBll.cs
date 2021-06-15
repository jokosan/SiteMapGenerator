using Microsoft.Extensions.DependencyInjection;

namespace SiteMapGenerator.Bll.Infrastructure.DependencyInjection
{
    public class DependencyResolverBll
    {
        public static void Initialize(IServiceCollection services)
        {
            DependencyResolverBusinessLogic.Initialize(services);
        }
    }
}
