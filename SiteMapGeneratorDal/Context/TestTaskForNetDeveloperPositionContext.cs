using Microsoft.EntityFrameworkCore;
using SiteMapGenerator.Dal.Models.Dal;
using System.Data;

#nullable disable

namespace SiteMapGeneratorDal.dbContext
{
    public partial class TestTaskForNetDeveloperPositionContext : DbContext, IEfRepositoryDbContext
    {

        public TestTaskForNetDeveloperPositionContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ArchiveOfRequest> ArchiveOfRequests { get; set; }
        public DbSet<PageInfo> PageInfos { get; set; }
        public DbSet<UrlSiteMap> UrlSiteMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestTaskForNetDeveloperPositionContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
