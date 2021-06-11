using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SiteMapGeneratorDal.Migrations
{
    public partial class SiteMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchiveOfRequests",
                columns: table => new
                {
                    IdArchiveOfRequests = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveOfRequests", x => x.IdArchiveOfRequests);
                });

            migrationBuilder.CreateTable(
                name: "PageInfos",
                columns: table => new
                {
                    IdPageInfo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SitemapId = table.Column<int>(type: "int", nullable: true),
                    WebsiteLoadingSpeed = table.Column<long>(type: "bigint", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: true),
                    PageTestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Elapsed = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageInfos", x => x.IdPageInfo);
                });

            migrationBuilder.CreateTable(
                name: "UrlSiteMaps",
                columns: table => new
                {
                    IdSitemap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArchiveOfRequestsId = table.Column<int>(type: "int", nullable: true),
                    NameSite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlSiteMaps", x => x.IdSitemap);
                    table.ForeignKey(
                        name: "FK_UrlSiteMaps_ArchiveOfRequests_ArchiveOfRequestsId",
                        column: x => x.ArchiveOfRequestsId,
                        principalTable: "ArchiveOfRequests",
                        principalColumn: "IdArchiveOfRequests",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlSiteMaps_ArchiveOfRequestsId",
                table: "UrlSiteMaps",
                column: "ArchiveOfRequestsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageInfos");

            migrationBuilder.DropTable(
                name: "UrlSiteMaps");

            migrationBuilder.DropTable(
                name: "ArchiveOfRequests");
        }
    }
}
