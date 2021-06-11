using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiteMapGeneratorDal.dbContext;

namespace SiteMapGeneratorDal.Utilities
{
    public class DbContextServiceCollectionDal
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddEfRepository<TestTaskForNetDeveloperPositionContext>(options => options.UseSqlServer(@"Server=LAPTOP-DN88AAG5\SQLEXPRESS; initial catalog=TestTaskForNetDeveloperPosition; integrated security=True;MultipleActiveResultSets=True"));

        }
    }
}
