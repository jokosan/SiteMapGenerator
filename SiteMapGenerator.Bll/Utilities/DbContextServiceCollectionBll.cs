using Microsoft.Extensions.DependencyInjection;
using SiteMapGeneratorDal.Utilities;

namespace SiteMapGenerator.Bll.Utilities
{
    public class DbContextServiceCollectionBll
    {
        public static void Initialize(IServiceCollection services)
        {
            DbContextServiceCollectionDal.Initialize(services);
        }
    }
}
