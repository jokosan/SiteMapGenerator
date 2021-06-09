using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SiteMapGeneratorDal.dbContext
{
    public partial class TestTaskForNetDeveloperPositionContext : DbContext
    {
        public TestTaskForNetDeveloperPositionContext()
        {
        }

        public TestTaskForNetDeveloperPositionContext(DbContextOptions<TestTaskForNetDeveloperPositionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArchiveOfRequest> ArchiveOfRequests { get; set; }
        public virtual DbSet<PageInfo> PageInfos { get; set; }
        public virtual DbSet<UrlSiteMap> UrlSiteMaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-DN88AAG5\\SQLEXPRESS;Database=TestTaskForNetDeveloperPosition;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<ArchiveOfRequest>(entity =>
            {
                entity.HasKey(e => e.IdArchiveOfRequests);
            });

            modelBuilder.Entity<PageInfo>(entity =>
            {
                entity.HasKey(e => e.IdPageInfo);

                entity.ToTable("PageInfo");
            });

            modelBuilder.Entity<UrlSiteMap>(entity =>
            {
                entity.HasKey(e => e.IdSitemap);

                entity.ToTable("UrlSiteMap");

                entity.HasOne(d => d.ArchiveOfRequests)
                    .WithMany(p => p.UrlSiteMaps)
                    .HasForeignKey(d => d.ArchiveOfRequestsId)
                    .HasConstraintName("FK_UrlSiteMap_ArchiveOfRequests");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
