using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Dal.dbContext;

namespace SiteMapGenerator.Dal.Utilities
{
    public class DbContextServiceCollectionDal
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddEfRepository<SitMapDbContext>(options => options.UseSqlServer(@"Server=LAPTOP-DN88AAG5\SQLEXPRESS; initial catalog=SitMapDb; integrated security=True;MultipleActiveResultSets=True"));
        }
    }
}
