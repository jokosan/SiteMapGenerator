using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Dal.dbContext;

namespace SiteMapGenerator.Dal.Utilities
{
    public class DbContextServiceCollectionDal
    {
        public static void Initialize(IServiceCollection services, string ConnectionString)
        {
            services.AddEfRepository<SitMapDbContext>(options => options.UseSqlServer(ConnectionString));
        }
    }
}
