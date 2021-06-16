using Microsoft.EntityFrameworkCore;
using SiteMapGenerator.Dal.Models.Dal;
using System.Data;

#nullable disable

namespace SiteMapGenerator.Dal.dbContext
{
    public partial class SitMapDbContext : DbContext, IEfRepositoryDbContext
    {

        public SitMapDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ArchiveOfRequest> ArchiveOfRequests { get; set; }
        public DbSet<PageInfo> PageInfos { get; set; }
        public DbSet<UrlSiteMap> UrlSiteMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SitMapDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
