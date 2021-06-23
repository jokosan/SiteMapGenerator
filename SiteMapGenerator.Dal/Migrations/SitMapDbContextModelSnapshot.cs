﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SiteMapGenerator.Dal.dbContext;

namespace SiteMapGenerator.Dal.Migrations
{
    [DbContext(typeof(SitMapDbContext))]
    partial class SitMapDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SiteMapGenerator.Dal.Models.Dal.ArchiveOfRequest", b =>
                {
                    b.Property<int>("IdArchiveOfRequests")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdArchiveOfRequests");

                    b.ToTable("ArchiveOfRequests");
                });

            modelBuilder.Entity("SiteMapGenerator.Dal.Models.Dal.PageInfo", b =>
                {
                    b.Property<int>("IdPageInfo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Elapsed")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PageTestDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SitemapId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusCode")
                        .HasColumnType("int");

                    b.Property<long?>("WebsiteLoadingSpeed")
                        .HasColumnType("bigint");

                    b.Property<bool>("parseLink")
                        .HasColumnType("bit");

                    b.Property<bool>("sitemapLink")
                        .HasColumnType("bit");

                    b.HasKey("IdPageInfo");

                    b.ToTable("PageInfos");
                });

            modelBuilder.Entity("SiteMapGenerator.Dal.Models.Dal.UrlSiteMap", b =>
                {
                    b.Property<int>("IdSitemap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArchiveOfRequestsId")
                        .HasColumnType("int");

                    b.Property<string>("NameSite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSitemap");

                    b.HasIndex("ArchiveOfRequestsId");

                    b.ToTable("UrlSiteMaps");
                });

            modelBuilder.Entity("SiteMapGenerator.Dal.Models.Dal.UrlSiteMap", b =>
                {
                    b.HasOne("SiteMapGenerator.Dal.Models.Dal.ArchiveOfRequest", "ArchiveOfRequests")
                        .WithMany("UrlSiteMaps")
                        .HasForeignKey("ArchiveOfRequestsId");

                    b.Navigation("ArchiveOfRequests");
                });

            modelBuilder.Entity("SiteMapGenerator.Dal.Models.Dal.ArchiveOfRequest", b =>
                {
                    b.Navigation("UrlSiteMaps");
                });
#pragma warning restore 612, 618
        }
    }
}
