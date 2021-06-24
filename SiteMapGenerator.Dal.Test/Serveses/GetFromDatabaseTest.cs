using Moq;
using SiteMapGenerator.Dal.Models.Dal;
using SiteMapGenerator.Dal.Serveses;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace SiteMapGenerator.Dal.Test.Serveses
{
    public class GetFromDatabaseTest
    {
        Mock<IRepository<ArchiveOfRequest>> mockArchiveOfRequest = new Mock<IRepository<ArchiveOfRequest>>();
        Mock<IRepository<PageInfo>> mockPageInfo = new Mock<IRepository<PageInfo>>();
        Mock<IRepository<UrlSiteMap>> mockUrlSiteMap = new Mock<IRepository<UrlSiteMap>>();

        List<UrlSiteMap> resulUrlSiteMap = new List<UrlSiteMap>() {
                 new UrlSiteMap {IdSitemap = 1, ArchiveOfRequestsId = 1, NameSite = "https://docs.microsoft.com/" },
                 new UrlSiteMap { IdSitemap = 2, ArchiveOfRequestsId = 1, NameSite = "https://docs.microsoft.com/main" },
                 new UrlSiteMap { IdSitemap = 3, ArchiveOfRequestsId = 1, NameSite = "https://docs.microsoft.com/en-us/cpp/" }
            };

        List<PageInfo> resulPageInfo = new List<PageInfo>() {
                new PageInfo { IdPageInfo = 1, SitemapId = 1, WebsiteLoadingSpeed = 3725757, StatusCode = 200, Elapsed = 12 },
                new PageInfo { IdPageInfo = 2, SitemapId = 2, WebsiteLoadingSpeed = 3325757, StatusCode = 200, Elapsed = 12  },
                new PageInfo { IdPageInfo = 3, SitemapId = 1, WebsiteLoadingSpeed = 3325757, StatusCode = 200, Elapsed = 12  }
            };

        [Fact]
        public void GetArchiveOfRequest_GetArchiveOfRequestTableFromatabase_ReturnTable()
        {
            // Arrange
            var serveses = new GetFromDatabase(mockArchiveOfRequest.Object, mockPageInfo.Object, mockUrlSiteMap.Object);

            var resultList = new List<ArchiveOfRequest>() {
                new ArchiveOfRequest{IdArchiveOfRequests = 1, NameUrl = "http://test1.com/" },
                new ArchiveOfRequest{ IdArchiveOfRequests = 2, NameUrl = "http://test2.com/"}
            }.AsQueryable();

            mockArchiveOfRequest.Setup(x => x.GetAll()).Returns(resultList);

            // Act
            var result = serveses.GetArchiveOfRequest();

            //Assert
            Assert.Equal(resultList, result);
        }

        [Fact]
        public void GetSiteMap_GetUrlSiteMapTableFromatabase_ReturnTable()
        {
            // Arrange
            var serveses = new GetFromDatabase(mockArchiveOfRequest.Object, mockPageInfo.Object, mockUrlSiteMap.Object);

            mockUrlSiteMap.Setup(x => x.GetAll()).Returns(resulUrlSiteMap.AsQueryable());

            // Act
            var result = serveses.GetSiteMap();

            // Assert
            Assert.Equal(resulUrlSiteMap, result);
        }

        [Fact]
        public void GetPageInfos_GetPageInfoTableFromatabase_ReturnTable()
        {
            // Arrange
            var serveses = new GetFromDatabase(mockArchiveOfRequest.Object, mockPageInfo.Object, mockUrlSiteMap.Object);

            mockPageInfo.Setup(x => x.GetAll()).Returns(resulPageInfo.AsQueryable);

            // Act
            var result = serveses.GetPageInfos();

            // Assert
            Assert.Equal(resulPageInfo, result);
        }
    }
}
