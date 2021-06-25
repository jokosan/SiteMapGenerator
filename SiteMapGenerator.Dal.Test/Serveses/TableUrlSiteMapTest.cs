using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SiteMapGenerator.Dal.Models.Dal;
using SiteMapGenerator.Dal.Serveses;
using Xunit;

namespace SiteMapGenerator.Dal.Test.Serveses
{
    public class TableUrlSiteMapTest
    {
        Mock<IRepository<UrlSiteMap>> mockUrlSiteMap = new Mock<IRepository<UrlSiteMap>>();

        List<UrlSiteMap> resulUrlSiteMap = new List<UrlSiteMap>() {
                 new UrlSiteMap {IdSitemap = 1, ArchiveOfRequestsId = 1, NameSite = "https://docs.microsoft.com/" },
                 new UrlSiteMap { IdSitemap = 2, ArchiveOfRequestsId = 1, NameSite = "https://docs.microsoft.com/main" },
                 new UrlSiteMap { IdSitemap = 3, ArchiveOfRequestsId = 1, NameSite = "https://docs.microsoft.com/en-us/cpp/" },
                 new UrlSiteMap { IdSitemap = 4, ArchiveOfRequestsId = 2, NameSite = "https://microsoft.com/en-us/" }
            };

        [Fact]
        public void GetSiteMap_GetUrlSiteMapTableFromatabase_ReturnTable()
        {
            // Arrange
            var serveses = new TableUrlSiteMap(mockUrlSiteMap.Object);

            mockUrlSiteMap.Setup(x => x.GetAll()).Returns(resulUrlSiteMap.AsQueryable());

            // Act
            var result = serveses.GetSiteMap();

            // Assert
            Assert.Equal(resulUrlSiteMap, result);
        }

        [Fact]
        public void SearchLinkBd__ReturnTrue()
        {
            // Arrange
            var serveses = new TableUrlSiteMap(mockUrlSiteMap.Object);
            mockUrlSiteMap.SetupSequence(x => x.GetAll()).Returns(resulUrlSiteMap.AsQueryable());

            // Act
            var result = serveses.SearchLinkBd("https://docs.microsoft.com/en-us/cpp/");

            // Assert
            Assert.Equal(result, true);
        }

        [Fact]
        public void SearchLinkBd__ReturnFalse()
        {
            // Arrange
            var serveses = new TableUrlSiteMap(mockUrlSiteMap.Object);
            mockUrlSiteMap.SetupSequence(x => x.GetAll()).Returns(resulUrlSiteMap.AsQueryable());

            // Act
            var result = serveses.SearchLinkBd("https://docs.microsoft.com/test/");

            // Assert
            Assert.Equal(result, false);
        }

        [Fact]
        public void RequestToGetMatchesForGiven_()
        {
            // Arrange
            var serveses = new TableUrlSiteMap(mockUrlSiteMap.Object);
            mockUrlSiteMap.SetupSequence(x => x.GetAll()).Returns(resulUrlSiteMap.AsQueryable());

            // Act
            var result = serveses.RequestToGetMatchesForGiven(2);

            // Assert
            Assert.Equal(result.ToList()[0].NameSite, "https://microsoft.com/en-us/");
        }

        [Fact]
        public void SaveSitemap_()
        {
            // Arrange
            var serveses = new TableUrlSiteMap(mockUrlSiteMap.Object);
            mockUrlSiteMap.Setup(_ => _.SaveChanges());

            // Act
            var result = serveses.SaveSitemap(resulUrlSiteMap[0]);

            // Assert
            Assert.Equal(result, 1);
            mockUrlSiteMap.Verify(_ => _.Add(resulUrlSiteMap[0]), Times.AtLeastOnce());
        }
    }
}
