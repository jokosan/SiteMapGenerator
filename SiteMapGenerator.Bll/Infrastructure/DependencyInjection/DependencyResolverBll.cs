using Microsoft.Extensions.DependencyInjection;
using SiteMapGeneratorDal.Infrastructure.DependencyInjection;

namespace SiteMapGenerator.Bll.Infrastructure.DependencyInjection
{
    public class DependencyResolverBll
    {
        public static void Initialize(IServiceCollection services)
        {
            DependencyResolverBusinessLogic.Initialize(services);
            DependencyResolverServeses.Initialize(services);
            DependencyInjectionDal.Initialize(services);
        }
    }
}
